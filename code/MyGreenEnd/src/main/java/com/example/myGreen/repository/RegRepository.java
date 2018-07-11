package com.example.myGreen.repository;

import com.example.myGreen.entity.Register;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

@Repository
@Table(name = "REGISTER")
@Qualifier("regRepository")
public interface RegRepository extends JpaRepository<Register, Long> {
    public Register findByUsername(String username);

    public Register findByToken(String token);
}