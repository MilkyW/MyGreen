package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.Reg;

@Repository
@Table(name="Reg")
@Qualifier("regRepository")
public interface RegRepository extends JpaRepository<Reg, Long>{
    public Reg findByAccount(String account);
    public Reg findByToken(String token);
}
