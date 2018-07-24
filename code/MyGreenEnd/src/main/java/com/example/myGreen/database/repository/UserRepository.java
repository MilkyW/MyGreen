package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.User;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import javax.persistence.Table;

@Repository
@Table(name = "USER")
@Qualifier("userRepository")
public interface UserRepository extends JpaRepository<User, Long> {

    User findByUsername(String username);

    User findByPhone(String phone);

    User findByEmail(String email);

    @Query("select enabled from User t where t.username=:username")
    boolean findEnabledByUsername(@Param("username") String username);

    @Transactional
    @Query(value = "update User set enabled=:enabled where id=:id", nativeQuery = true)
    void updateEnabledById(@Param("id") long id, @Param("enabled") boolean enabled);
}