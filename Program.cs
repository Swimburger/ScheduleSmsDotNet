using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

string twilioAccountSid = Environment.GetEnvironmentVariable("TwilioAccountSid");
string twilioAuthToken = Environment.GetEnvironmentVariable("TwilioAuthToken");
string twilioMessagingServiceSid = Environment.GetEnvironmentVariable("TwilioMessagingServiceSid");
string toPhoneNumber = Environment.GetEnvironmentVariable("ToPhoneNumber");

TwilioClient.Init(
    twilioAccountSid,
    twilioAuthToken
);

Console.Write("Do you want to schedule an SMS? (y/n)");
if (string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase))
{
    var message = MessageResource.Create(
        messagingServiceSid: twilioMessagingServiceSid,
        body: "Hi there",
        to: new PhoneNumber(toPhoneNumber),
        sendAt: DateTime.UtcNow.AddMinutes(61),
        scheduleType: MessageResource.ScheduleTypeEnum.Fixed
    );
    Console.WriteLine($"Message SID: {message.Sid}");
}

Console.Write("Do you want to cancel a scheduled SMS? (y/n)");
if (string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase))
{
    Console.Write("Enter message SID:");
    string messageSid = Console.ReadLine();

    var messages = MessageResource.Update(pathSid: messageSid, status: MessageResource.UpdateStatusEnum.Canceled);
    Console.WriteLine($"Scheduled SMS cancelled");
}