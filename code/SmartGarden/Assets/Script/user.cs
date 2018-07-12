using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user {

    private long id;
    private string username;
    private string nickname;
    private string password;
    private string firstname;
    private string lastname;
    private bool gender;
    private string phone;
    private string email;
    private bool enabled;
    private List<m_garden> m_Gardens;

    public user()
    {
        m_Gardens = new List<m_garden>();
    }

    public long getId() { return id; }

    public string getUsername() { return username; }

    public string getNickname() { return nickname; }

    public string getPassword() { return password; }

    public string getFirstname() { return firstname; }

    public string getLastname() { return lastname; }

    public bool getGender() { return gender; }

    public string getPhone() { return phone; }

    public string getEmail() { return email; }

    public List<m_garden> getGardens() { return m_Gardens; }

    public m_garden getGardenByGardenId(long gardenId)
    {
        foreach (m_garden e in m_Gardens)
            if (e.getId() == gardenId)
                return e;
        return null;
    }

    public void setId(long id_) { id = id_; }

    public void setUsername(string username_) { username = username_; }

    public void setNickname(string nickname_) { nickname = nickname_; }

    public void setPassword(string password_) { password = password_; }

    public void setFirstname(string firstname_) { firstname = firstname_; }

    public void setLastname(string lastname_) { lastname = lastname_; }

    public void setGender(bool gender_) { gender = gender_; }

    public void setPhone(string phone_) { phone = phone_; }

    public void setEmail(string email_) { email = email_; }

    public void setGardens(List<m_garden> m_Gardens_) { m_Gardens = m_Gardens_; }

    public void addGardens(m_garden m_Garden) { m_Gardens.Add(m_Garden); }
}
