package com.example.myGreen.database.repository;

import com.example.myGreen.database.entity.Register;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

@Repository
@Table(name = "REGISTER")
@Qualifier("regRepository")
public interface RegisterRepository extends JpaRepository<Register, Long> {

    Register findByToken(String token);
}