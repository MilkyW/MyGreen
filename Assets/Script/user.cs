using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user {

    private string username;
    private string nickname;
    private string password;
    private string firstname;
    private string lastname;
    private string gender;
    private string phone;
    private string email;

    public string getUsername() { return username; }

    public string getNickname() { return nickname; }

    public string getPassword() { return password; }

    public string getFirstname() { return firstname; }

    public string getLastname() { return lastname; }

    public string getGender() { return gender; }

    public string getPhone() { return phone; }

    public string getEmail() { return email; }

    public void setUsername(string username_) { username = username_; }

    public void setNickname(string nickname_) { nickname = nickname_; }

    public void setPassword(string password_) { password = password_; }

    public void setFirstname(string firstname_) { firstname = firstname_; }

    public void setLastname(string lastname_) { lastname = lastname_; }

    public void setGender(string gender_) { gender = gender_; }

    public void setPhone(string phone_) { phone = phone_; }

    public void setEmail(string email_) { email = email_; }
}
