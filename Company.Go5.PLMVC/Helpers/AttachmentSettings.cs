using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Company.Go5.PLMVC.Helpers
{
    
    public class AttachmentSettings
    {
        //upload method 


        public static string Upload(IFormFile file, string foldername)
        {

            // get folder path

           // var folderspath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername);


            //get file name and make it unique using merging with new guid 

            var filename = $"{Guid.NewGuid()}{file.FileName}";

            //  var filepath = Path.Combine(folderspath, filename);
         
            
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername, filename);


            //using the filepath by file stream then copy file to file path  
            using var filestream = new FileStream(filepath, FileMode.Create);

            file.CopyTo(filestream);





            return filename;


        }




        public static void Unload(string filename,string foldername)
        {

        

            var filepath=Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername, filename);


            if (File.Exists(filepath))
            {
                File.Delete(filepath);

            }



        }

    }
}
