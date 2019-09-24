import Vue from 'vue'
import App from './App.vue'

import axios from 'axios'

//const Employee = (EmployeeId, SurName, Name, Patronimyc, Email) => ({EmployeeId, SurName, Name, Patronimyc, Email})
//const Project = (EmployeeId, SurName, Name, Patronimyc, Email) => ({EmployeeId, SurName, Name, Patronimyc, Email})

Vue.component('viewproject',{
  data: function(){
    return{

    }
  },
  template : `
  <div class="container">
    <ul class="list-group">
      <li class="list-group-item">
        <div class="row">
          <label>Название компании-заказчика</label>
          <input type="text" readonly name="" id="projectCustomer">
        </div>
      </li>
      <li class="list-group-item">
        <div class="row">
          <label>Название компании-исполнителя</label>
          <input type="text" readonly name="" id="projectPerformer">
        </div>
      </li>
      <li class="list-group-item">
        <div class="row">
          <label>Руководитель проекта</label>
          <input type="text" readonly name="" id="projectLead">
        </div>
      </li>
      <li class="list-group-item">
        <div class="row">
          <label>Дата начала проекта</label>
          <input type="text" readonly name="" id="projectStart">
        </div>
      </li>
      <li class="list-group-item">
        <div class="row">
          <label>Дата окончания проекта</label>
          <input type="text" readonly name="" id="projectFinish">
        </div>
      </li>
      <li class="list-group-item">
        <div class="row">
          <label>Приоритет проекта</label>
          <input type="text" readonly name="" id="projectPriority">
        </div>
      </li>
    </ul>
  </div>
`
});


new Vue({
  el: '#app',
  data: {
    employees: [],
    projects: []
  },

  component:
  {
    project: {
      template: '#projectInfo'/*
      Name: {
        template: '#projectName'
      },
      Customer: {
        template: '#projectCustomer'
      },
      Performer: {
        template: '#projectPerformer'
      },
      Lead: {
        template: '#projectLead'
      },
      Start: {
        template: '#projectStart'
      },
      Finish: {
        template: '#projectFinish'
      },
      Priority: {
        template: '#projectPriority'
      }*/
    }
  },

  created()
  {
      axios.get('https://localhost:44316/api/employee/')
      .then(response => {
          this.employees = response.data,
          console.log(this.employees)
      })
      .catch(error => {
          console.log(error)
      }),
      axios.get('https://localhost:44316/api/project/projects')
      .then(response => {
          this.projects = response.data,
          console.log(this.projects)
      })
      .catch(error => {
          console.log(error)
      })
  },
  methods:{
    GetLead (leadId, employees){
      console.log("LEADID:"+leadId);
      var a = "Выбрать руководителя";
      employees.some(element => {
        if(element.employeeId == leadId){
            console.log("IN CIKL"+element.employeeId + " " + leadId);
            return a = element.name;
            
          }
        });
      return a;  
    },

    GetProjectById(event)
    {
       var a;
        this.projects.some(element =>{
          if(element.projectId == event.target.id)
            return a = element;
          })
      return a;
    },

    ViewProjectInfo(_project)
    {
      Project.Customer.value = project.Customer;
     /* Project.Performer = project.Performer;
      Project.Lead = project.Lead;
      Project.Start = project.Start;
      Project.Finish = project.Finish;
      Project.Priority = project.Priority;*/
    }


  }
  //render: h => h(App)
})
