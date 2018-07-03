package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import javax.persistence.Table;

import com.example.myGreen.entity.Controller;

@Repository
@Table(name = "CONTROLLER")
@Qualifier("controllerRepository")
public interface ControllerRepository extends JpaRepository<Controller, Long> {


}