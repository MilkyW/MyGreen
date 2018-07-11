package com.example.myGreen.repository;

import com.example.myGreen.entity.User;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

@Repository
@Table(name = "USER")
@Qualifier("userRepository")
public interface UserRepository extends JpaRepository<User, Long> {

    public User findByUsername(String username);

    public User findByPhone(String phone);

    public User findByEmail(String email);

    @Query("select enabled from User t where t.username=:username")
    public boolean findEnabledByUsername(@Param("username") String username);
}