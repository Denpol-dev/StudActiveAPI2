using Microsoft.EntityFrameworkCore;
using StudActiveAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudActiveAPI.Models
{
    public partial class StudentStudActive
    {
        [Key]
        public Guid StudActiveId { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ReEntryDate { get; set; }
        public bool IsArchive { get; set; }
        public Guid? RoleActive { get; set; }
        public string VkLink { get; set; }
        public Guid StudentId { get; set; }
        public Guid StudentCouncilId { get; set; }
    }

    public class StudentsActiveModel
    {
        public Guid Id { get; set; }
        public string CouncilName { get; set; }
        public string Fio { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ReEntryDate { get; set; }
        public bool IsArchive { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
        public string Sex { get; set; }
        public string MobilePhone { get; set; }
        public string VkLink { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid StudentId { get; set; }

        public static async Task<StudentsActiveModel> GetStudentActive(Guid userId)
        {
            using var context = new Context();
            var result = new StudentsActiveModel();
            var userStudents = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);
            var studentId = userStudents.StudentId;

            var groups = await context.Groups.ToListAsync();
            var studActiv = await context.StudentStudActives.FirstOrDefaultAsync(x => x.StudentId == studentId);
            var students = await context.Students.ToListAsync();

            if (studActiv != null)
            {
                var student = students.FirstOrDefault(x => x.StudentId == studActiv.StudentId);
                var groupId = student.GroupId;
                var group = groups.FirstOrDefault(x => x.GroupId == groupId);

                string sex = student.Sex.ToString();

                if (sex == "0")
                    sex = "Ж";
                else
                    sex = "М";

                Guid roleId = studActiv.RoleActive.GetValueOrDefault();
                var role = await context.RoleStudActives.FirstOrDefaultAsync(x => x.Id == roleId);
                string roleName = role.Name;

                if (roleName == "Member")
                    roleName = "Рядовой";
                else if (roleName == "Chairman")
                    roleName = "Председатель";
                else if (roleName == "ViceChairman")
                    roleName = "Зам. председателя";

                var council = await context.StudentCouncils.FirstOrDefaultAsync(x => x.StudentCouncilId == studActiv.StudentCouncilId);

                long phoneString = Convert.ToInt64(student.MobilePhoneNumber);
                result = new StudentsActiveModel
                {
                    Id = studActiv.StudentId,
                    Fio = student.LastName + " " + student.FirstName + " " + student.MiddleName,
                    EntryDate = studActiv.EntryDate,
                    LeavingDate = studActiv.LeavingDate,
                    ReEntryDate = studActiv.ReEntryDate,
                    IsArchive = studActiv.IsArchive,
                    RoleId = roleId,
                    Role = roleName,
                    Sex = sex,
                    MobilePhone = string.Format("{0:+7 (###) ###-##-##}", phoneString),
                    BirthDate = student.BirthDate,
                    VkLink = "https://vk.com/" + studActiv.VkLink,
                    GroupId = group.GroupId,
                    GroupName = group.Name + "-" + group.CourseNumber + group.Number,
                    CouncilName = council.Name
                };
                return result;
            }
            return null;
        }

        public static async Task<bool> ChangePass(string plainText, Guid id)
        {
            var saltHash = GetHashPass(plainText);

            using var context = new Context();
            var authorizedUser = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            var hashSalt = saltHash.Item1;
            var salt = saltHash.Item2;

            authorizedUser.Hash = hashSalt;
            authorizedUser.Salt = salt;

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Tuple<string, byte[]> GetHashPass(string plainText)
        {
            var random = new Random();
            var saltSize = random.Next(4, 8);
            var salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var text = plainText;
            var textBytes = Encoding.UTF8.GetBytes(text);
            var textBytesSalt = new byte[textBytes.Length + salt.Length];

            for (int i = 0; i < textBytes.Length; i++)
                textBytesSalt[i] = textBytes[i];

            for (int i = 0; i < salt.Length; i++)
                textBytesSalt[textBytes.Length + i] = salt[i];

            HashAlgorithm hash = new SHA256Managed();

            var hashSaltBytes = hash.ComputeHash(textBytesSalt);
            var hastValue = Convert.ToBase64String(hashSaltBytes);

            return Tuple.Create(hastValue, salt);
        }
    }

    public class RegistrationStudActiveModel
    {
        public Guid StudActiveId { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public DateTime? ReEntryDate { get; set; }
        public bool IsArchive { get; set; }
        public Guid? RoleActive { get; set; }
        public string VkLink { get; set; }
        public Guid StudentId { get; set; }
        public Guid StudentCouncilId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid GroupId { get; set; }
        public int Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MobilePhoneNumber { get; set; }

        public async Task<bool> ChangeStudentActive()
        {
            using var context = new Context();
            var result = true;
            var studentActive = await context.StudentStudActives.Where(s => s.StudActiveId == StudActiveId).FirstOrDefaultAsync();

            if (studentActive != null)
            {
                studentActive.EntryDate = EntryDate;
                studentActive.LeavingDate = LeavingDate;
                studentActive.ReEntryDate = ReEntryDate;
                studentActive.IsArchive = IsArchive;
                studentActive.RoleActive = RoleActive;
                studentActive.VkLink = VkLink;
                studentActive.StudentId = StudentId;
                studentActive.StudentCouncilId = StudentCouncilId;
            }

            var student = await context.Students.Where(s => s.StudentId == StudentId).FirstOrDefaultAsync();

            if (student != null)
            {
                student.GroupId = GroupId;
                student.FirstName = FirstName;
                student.LastName = LastName;
                student.MiddleName = MiddleName;
                student.Sex = Sex;
                student.BirthDate = BirthDate;
                student.MobilePhoneNumber = MobilePhoneNumber;
            }

            var user = await context.Users.Where(u => u.Id == student.UserId).FirstOrDefaultAsync();

            if (user != null)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.MiddleName = MiddleName;
            }

            await context.SaveChangesAsync();

            return result;
        }
    }
}