namespace Domain.Shared
{
    public static class ValidationErrorMessages
    {
        #region Validation Company Error Messages
        public static string ErrorCompanyNameAlreadyExist(string companyName) => $"Company with name {companyName} already exist ";
        public static string ErrorCompanyAddress => $"Company address must be at least 5 characters ";
        public static string ErrorCompanyIsNotExist(int id) => $"Company with id : {id} does not exist ";
        public static string ErrorCompanyHaveUsers => $"Company have users so it can't be deleted";


        #endregion

        #region Validation User Error Messages
        public static string ErrorEmailAlreadyTaken(string email) => $"User with email : {email} already taken.";
        public static string ErrorUserIsNotExist(int userId) => $"User with id : {userId} does not exist.";
        public static string ErrorInvalidPassword => $"Password must be at least 8 characters with 1 numeric digit and 1 capital letters";

        #endregion

    }
}
