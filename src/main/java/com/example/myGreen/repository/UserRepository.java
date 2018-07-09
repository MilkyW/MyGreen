package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.User;

@Repository
@Table(name="USER")
@Qualifier("userRepository")
public interface UserRepository extends JpaRepository<User, Long>{

    public User findByAccount(String account);

    public User findByPhone(String phone);

    public User findByEmail(String email);

    @Query("select valid from User t where t.account=:account")
    public boolean findValidByAccount(@Param("account") String account);
}