package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;

import com.example.myGreen.entity.WetnessSensor;

@Repository
@Table(name = "WETNESSSENSOR")
@Qualifier("wetnessSensorRepository")
public interface WetnessSensorRepository extends JpaRepository<WetnessSensor, Long> {


}