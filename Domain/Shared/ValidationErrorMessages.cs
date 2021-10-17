namespace Domain.Shared
{
    public static class ValidationErrorMessages
    {
        #region Validation Company Error Messages
        public static string ErrorCompanyNameAlreadyExist(string companyName) => $"Company with name {companyName} already exist ";
        public static string ErrorCompanyAddress => $"Company address should be more than 5 ";
        public static string ErrorCompanyIsNotExist(int id) => $"Company with id : {id} does not exist ";

        #endregion

        #region Validation User Error Messages
        public static string ErrorEmailAlreadyTaken(string email) => $"User with email : {email} already taken.";
        public static string ErrorUserIsNotExist(int userId) => $"User with id : {userId} does not exist.";
        #endregion

    }
}
