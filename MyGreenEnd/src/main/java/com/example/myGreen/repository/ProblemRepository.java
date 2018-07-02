package com.example.myGreen.repository;

import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import javax.persistence.Table;
import java.util.List;

import com.example.myGreen.entity.Problem;

@Repository
@Table(name = "PROBLEM")
@Qualifier("problemRepository")
public interface ProblemRepository extends JpaRepository<Problem, Long> {

    @Query("select t from Problem t where t.title=:title")
    public List<Problem> findByName(@Param("title") String title);
}
