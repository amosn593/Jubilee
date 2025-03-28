﻿using Hangfire;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DAL.DataContext;
using DOMAIN.Interface;
using DOMAIN.Models;
using DOMAIN.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestSharp;
using Microsoft.Extensions.Logging;

namespace DAL.Services.Repository;
public class TransactionRepo : ITransactionRepo
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<TransactionRepo> _logger;
    private readonly MpesaSetting _options;
    private readonly HttpClient _httpClient;


    public TransactionRepo(AppDbContext appDbContext, ILogger<TransactionRepo> logger,
        IHttpClientFactory httpClientFactory, IOptions<MpesaSetting> options)
    {
        _appDbContext = appDbContext;
        _logger = logger;
        _options = options.Value;
        _httpClient = httpClientFactory.CreateClient("Mpesa");
    }

    public async Task<bool> Deposit(TransactionDto transaction, int UserId)
    {
        try
        {
            //Add Basic Auth
            var authenticationString = $"{_options.ConsumerKey}:{_options.ConsumerSecret}";

            var tokenB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));

            //Check In Db If there is access Token
            var AccessToken = await _appDbContext.AccessTokenResponses
                .OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (AccessToken == null || AccessToken.ExpireDate < DateTime.Now)
            {
                //Make call
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", tokenB64);

                var Response = await _httpClient.GetAsync("/oauth/v1/generate?grant_type=client_credentials");

                var ResponseContent = await Response.Content.ReadFromJsonAsync<AccessTokenResponse>();


                //Remove Current Token

                if (AccessToken != null)
                {
                    _appDbContext.AccessTokenResponses.Remove(AccessToken);
                }

                ResponseContent!.CreatedDate = DateTime.Now;
                ResponseContent.ExpireDate = DateTime.Now.AddSeconds((double)ResponseContent.expires_in!);

                await _appDbContext.AccessTokenResponses.AddAsync(ResponseContent);

                await _appDbContext.SaveChangesAsync();

                //Return results
                AccessToken = ResponseContent;
            }


            //Get Token
            var Token = AccessToken;

            //Create New Instance of STKPush model and get password
            var stkPush = new StkPushModel
            {
                Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"),
                PartyB = _options.BusinessShortCode,
                BusinessShortCode = _options.BusinessShortCode,
                PartyA = transaction.PhoneNumber,
                PhoneNumber = transaction.PhoneNumber,
            };

            var plainTextBytes = Encoding.UTF8.GetBytes(stkPush.BusinessShortCode + $"{_options.PassKey}" + stkPush.Timestamp);

            stkPush.Password = Convert.ToBase64String(plainTextBytes);

            //Serialize data
            // Serialize the data to JSON
            var json = JsonSerializer.Serialize(stkPush);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);

            var ResponseApi = await _httpClient.PostAsync("/mpesa/stkpush/v1/processrequest", content);

            if (!ResponseApi.IsSuccessStatusCode)
            {
                throw new Exception(ResponseApi.ReasonPhrase);
                
            }

            var Results = await ResponseApi.Content.ReadFromJsonAsync<StkPushResponseModel>();
            
            var trans = new Transaction
            {
                Amount = transaction.Amount,
                PhoneNumber = transaction.PhoneNumber,
                TransDate = DateTime.Now,
                Status = "Complete",
                Type = "Deposit",
                UserId = UserId
            };

         
            await _appDbContext.Transactions.AddAsync(trans);

            await _appDbContext.SaveChangesAsync();

            await SetReminder(transaction, UserId);

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<List<Transaction>> GetTransaction(int UserId)
    {
        try
        {
            var result = await _appDbContext.Transactions
                .OrderBy(x => x.TransDate)
                .Include(x => x.user)
                .Where(x => x.UserId == UserId).ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<bool> SetReminder(TransactionDto transactionDto, int UserId)
    {
        try
        {
            if(!transactionDto.SetReminders)
            {
                return false;
            }
            if(transactionDto.Frequency == 0)
            {
                return false;
            }
            if(transactionDto.StartDate == null || transactionDto.StartDate <= DateTime.Now)
            {
                transactionDto.StartDate = DateTime.Now;
            }

            
            BackgroundJob.Schedule(() => RunReminder(transactionDto, UserId), TimeSpan.FromDays(transactionDto.Frequency));

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> WithDraw(WithdrawDto transaction, int UserId)
    {
        try
        {
            var trans = new Transaction
            {
                Amount = transaction.Amount,
                PhoneNumber = transaction.PhoneNumber,
                TransDate = DateTime.Now,
                Status = "Complete",
                Type = "Withdraw",
                UserId = UserId
            };
           
            await _appDbContext.Transactions.AddAsync(trans);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<bool> RunReminder(TransactionDto transactionDto, int UserId)
    {
        try
        {
            var smsConfig = await _appDbContext.SMSConfigModels.Where(x => x.SenderId == "AfricasTalking")
                                .FirstOrDefaultAsync();

            if(smsConfig == null)
            {
                return false;
            }

            var Link = $"https://dev-momo.tenzi.africa/api/payment/StkDeposit/{transactionDto.UserId}/{transactionDto.PhoneNumber}/{transactionDto.Amount}";

            var Message = $"Dear Customer, kindly pay for your Jubilee Investment account. Click this link, for Mpesa stk push. {Link}";


            var apikey = Encoding.UTF8.GetString(Convert.FromBase64String(smsConfig.Apikey));
            var smsUrl = Encoding.UTF8.GetString(Convert.FromBase64String(smsConfig.SMSURL));

            var client = new RestClient(smsUrl);
            var request = new RestRequest("/messaging", RestSharp.Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("apiKey", $"{apikey}");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            request.AddParameter("from", smsConfig.FromName);
            request.AddParameter("username", smsConfig.SenderName);
            request.AddParameter("to", transactionDto.PhoneNumber.ToString());
            request.AddParameter("message", Message);
            var apiResponse = await client.ExecutePostAsync<AfTalkingSmsMessageResponse>(request);

            _logger.LogWarning("Reminder Sent to 0702240787 for UserId: {UserId}, Api Response: {ApiResponse}", UserId,
                JsonSerializer.Serialize(apiResponse));

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while running the reminder for UserId: {UserId}", UserId);
            return false;
        }
    }

    public async Task<List<Bank?>> GetBanks()
    {
        try
        {
            var Results = await _appDbContext.Banks
                .Include(x => x.Branches)
                .ToListAsync();

            return Results;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<Transaction>> GetAllTransaction()
    {
        try
        {
            var result = await _appDbContext.Transactions
                .OrderBy(x => x.TransDate)
                .Include(x => x.user)
                .ToListAsync();
            return result;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }



}
