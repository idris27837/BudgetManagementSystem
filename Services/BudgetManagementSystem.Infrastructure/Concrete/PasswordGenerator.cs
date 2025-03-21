using BudgetManagementSystem.Infrastructure.Abstractions;
using BudgetManagementSystem.Infrastructure.CustomConfig;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace BudgetManagementSystem.Infrastructure.Concrete;

public sealed class PasswordGenerator : IPasswordGenerator
{
    public IOptions<PasswordGenSetting> PasswordGenSettings { get; }

    public string Password { get; set; }
    public bool IncludeLowerCase { get; set; }
    public bool IncludeUpperCase { get; set; }
    public bool IncludeNumeric { get; set; }
    public bool IncludeSpecial { get; set; }
    public bool IncludeSpaces { get; set; }
    public int LengthOfPassword { get; set; }


    public PasswordGenerator(
        IOptions<PasswordGenSetting> passwordGenSettings)
    {
        PasswordGenSettings = passwordGenSettings;
    }

    /// <summary>
    /// Sets Password Standards
    /// </summary>
    /// <param name="includeLowercase">Bool to say if lowercase are required</param>
    /// <param name="includeUppercase">Bool to say if uppercase are required</param>
    /// <param name="includeNumeric">Bool to say if numerics are required</param>
    /// <param name="includeSpecial">Bool to say if special characters are required</param>
    /// <param name="includeSpaces">Bool to say if spaces are required</param>
    /// <param name="lengthOfPassword">Length of password required. Should be between 8 and 128</param>
    /// <returns></returns>
    public string GeneratePassword(bool includeLowercase, bool includeUppercase,
        bool includeNumeric, bool includeSpecial, bool includeSpaces, int lengthOfPassword)
    {
        int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = int.Parse(PasswordGenSettings.Value.MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS);
        string LOWERCASE_CHARACTERS = PasswordGenSettings.Value.LOWERCASE_CHARACTERS;
        string UPPERCASE_CHARACTERS = PasswordGenSettings.Value.UPPERCASE_CHARACTERS;
        string NUMERIC_CHARACTERS = PasswordGenSettings.Value.NUMERIC_CHARACTERS;
        string SPECIAL_CHARACTERS = PasswordGenSettings.Value.SPECIAL_CHARACTERS;
        string SPACE_CHARACTER = PasswordGenSettings.Value.SPACE_CHARACTER;
        int PASSWORD_LENGTH_MIN = int.Parse(PasswordGenSettings.Value.PASSWORD_LENGTH_MIN);
        int PASSWORD_LENGTH_MAX = int.Parse(PasswordGenSettings.Value.PASSWORD_LENGTH_MAX); ;

        IncludeLowerCase = includeLowercase;
        IncludeUpperCase = includeUppercase;
        IncludeNumeric = includeNumeric;
        IncludeSpecial = includeSpecial;
        IncludeSpaces = includeSpaces;
        LengthOfPassword = lengthOfPassword;

        if (lengthOfPassword < PASSWORD_LENGTH_MIN || lengthOfPassword > PASSWORD_LENGTH_MAX)
        {
            throw new Exception($"Password length must be between {PASSWORD_LENGTH_MIN} and {PASSWORD_LENGTH_MAX}.");
        }

        string characterSet = "";

        if (IncludeLowerCase)
        {
            characterSet += LOWERCASE_CHARACTERS;
        }

        if (IncludeUpperCase)
        {
            characterSet += UPPERCASE_CHARACTERS;
        }

        if (IncludeNumeric)
        {
            characterSet += NUMERIC_CHARACTERS;
        }

        if (IncludeSpecial)
        {
            characterSet += SPECIAL_CHARACTERS;
        }

        if (IncludeSpaces)
        {
            characterSet += SPACE_CHARACTER;
        }

        char[] password = new char[lengthOfPassword];
        int characterSetLength = characterSet.Length;

        Random random = new Random();
        for (int characterPosition = 0; characterPosition < lengthOfPassword; characterPosition++)
        {
            password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

            bool moreThanTwoIdenticalInARow =
                characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                && password[characterPosition] == password[characterPosition - 1]
                && password[characterPosition - 1] == password[characterPosition - 2];

            if (moreThanTwoIdenticalInARow)
            {
                characterPosition--;
            }
        }

        Password = string.Join(null, password);
        return Password;
    }

    /// <summary>
    /// Checks if the password created is valid
    /// </summary>
    /// <param name="includeLowercase">Bool to say if lowercase are required</param>
    /// <param name="includeUppercase">Bool to say if uppercase are required</param>
    /// <param name="includeNumeric">Bool to say if numerics are required</param>
    /// <param name="includeSpecial">Bool to say if special characters are required</param>
    /// <param name="includeSpaces">Bool to say if spaces are required</param>
    /// <param name="password">Generated password</param>
    /// <returns>True or False to say if the password is valid or not</returns>
    public bool IsValid()
    {
        const string REGEX_LOWERCASE = @"[a-z]";
        const string REGEX_UPPERCASE = @"[A-Z]";
        const string REGEX_NUMERIC = @"[\d]";
        const string REGEX_SPECIAL = @"([!#$%&*@\\])+";
        const string REGEX_SPACE = @"([ ])+";

        bool lowerCaseIsValid = !IncludeLowerCase || IncludeLowerCase && Regex.IsMatch(Password, REGEX_LOWERCASE);
        bool upperCaseIsValid = !IncludeUpperCase || IncludeUpperCase && Regex.IsMatch(Password, REGEX_UPPERCASE);
        bool numericIsValid = !IncludeNumeric || IncludeNumeric && Regex.IsMatch(Password, REGEX_NUMERIC);
        bool symbolsAreValid = !IncludeSpecial || IncludeSpecial && Regex.IsMatch(Password, REGEX_SPECIAL);
        bool spacesAreValid = !IncludeSpaces || IncludeSpaces && Regex.IsMatch(Password, REGEX_SPACE);

        return lowerCaseIsValid && upperCaseIsValid && numericIsValid && symbolsAreValid && spacesAreValid;
    }
}

