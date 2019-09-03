using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace rating.Data
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Candidate {0} is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name Should be minimum 3 characters and a maximum of 100 characters")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }


        [Display(Name = "Phone No.")]
        [Required(ErrorMessage = "Candidate {0} is required")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "Candidate {0} is required")]
        public string Address { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Candidate {0} is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Candidate {0} is required")]       
        public string Password { get; set; }

        [Display(Name = "Centre")]
        [Required(ErrorMessage = "Candidate {0} is required")]
        public string Centre { get; set; }
        public bool isDone { get; set; }
        public int TimeLeft { get; set; }
        public bool isAdmin { get; set; }

        public virtual List<QuestionAndAnswer> QuestionAndAnswers { get; set; }


        public static List<User> GetAllUsers()
        {
            if (File.Exists(Constants.userPath))
            {
                var user_file = System.IO.File.ReadAllText(Constants.userPath);

                var jsonData = JsonConvert.DeserializeObject<List<User>>(user_file);

                return jsonData;
            }

            return null;
            
        }

        public static void AddUser(User user)
        {
            
            var users = GetAllUsers();

            users.Add(user);
            File.WriteAllText(Constants.userPath, JsonConvert.SerializeObject(users));
        }

        public static User GetUser(string password)
        {
            var users = GetAllUsers();

            var usr = users.Find(x => x.Password == password);

            return usr ?? null;
        }

    }
}
