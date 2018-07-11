package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.Register;

@Repository
@Table(name="REGISTER")
@Qualifier("regRepository")
public interface RegRepository extends JpaRepository<Register, Long>{
    public Register findByAccount(String account);
    public Register findByToken(String token);
}