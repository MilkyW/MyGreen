package com.example.myGreen.controller;

import com.example.myGreen.repository.ProblemRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import java.util.Iterator;
import java.util.List;
import java.util.ArrayList;
import net.sf.json.JSONArray;

import com.example.myGreen.entity.User;
import com.example.myGreen.entity.Problem;
import com.example.myGreen.repository.UserRepository;
import com.example.myGreen.repository.ProblemRepository;

@Controller
@RequestMapping("/MySQL")
@EnableAutoConfiguration
public class MySQLController {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private ProblemRepository problemRepository;

    @RequestMapping("getAllUser")
    @ResponseBody
    public String getAllUser() {
        return "";
    }

    @RequestMapping("getAllProblem")
    @ResponseBody
    public String getAllProblem() {
        ArrayList<JSONArray> problemsJson = new ArrayList<JSONArray>();

        /* Get problem */
        List<Problem> result = problemRepository.findAll();
        Iterator<Problem> it = result.iterator();
        while (it.hasNext()) {
            Problem problem = it.next();
            ArrayList<String> arrayList = new ArrayList<String>();

            arrayList.add(Long.toString(problem.getId()));
            arrayList.add(problem.getTitle());

            problemsJson.add(JSONArray.fromObject(arrayList));
        }
        JSONArray problems = JSONArray.fromArray(problemsJson.toArray());
        return problems.toString();
    }
}
