using System.Net;
using System.Net.Mail;

namespace Company.Go5.PLMVC.Helpers
{
    public static class EmailSettings
    {

        public static bool SendEmail(Email email)
        {
            try
            { //mail server : google
              //protocol : smtp

                var client = new SmtpClient("smtp.gmail.com", 587); //465 for ssl 

                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("popehab057@gmail.com",
                    "myzdmnfbyysdbwwr");

                //  myzdmnfbyysdbwwr :ex for  your app password of your gmail 


                client.Send("popehab057@gmail.com", email.To, email.Subject, email.Body);





                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
            
        }


    }
}
