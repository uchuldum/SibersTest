<template>
    <form @submit.prevent="AddProject" class="container">
        <ul class="list-group">
             <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Название проекта</label>
                    <input class="col-sm" required type="text" v-model="name" placeholder="Проект 1" >
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Название компании-заказчика</label>
                    <input class="col-sm" required type="text" v-model="customer"  placeholder="Apple" >
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Название компании-исполнителя</label>
                    <input class="col-sm" type="text" v-model="performer" required placeholder="Samsung">
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Руководитель проекта</label>
                    <findemployee inputType="radio" @lead="getLead" v-model="leadId" class="col-sm"></findemployee>
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Дата начала проекта</label>
                    <input type="date" required  v-model="startDate" class="col-sm"> 
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Дата окончания проекта</label>
                    <input  class="col-sm" type="date" required v-model="finishDate">
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class="col-sm">Приоритет проекта</label>
                    <select class="col-sm" v-model="selected">
                        <option v-for="option in options" v-bind:value="option">
                            {{option}}
                        </option>
                    </select>
                </div>
            </li>
            <li class="list-group-item">
                <div  class="row">
                    <label class="col-sm">Сотрудники</label>
                    <findemployee class="col-sm" @employees="getEmployees" inputType="checkbox"></findemployee>
                </div>
            </li>
            <input type="submit" class="btn btn-primary" value = "Добавить"/>
        </ul>
    </form>
</template>

<script>

import findemployee from './FindEmployee.vue';
import axios from 'axios';

export default {
    data(){
        return{
            name,
            options: ['0', '1', '2', '3', '4', '5'],
            selected: '0',
            today: Date,
            customer: "",
            performer:"",
            leadId: null,
            startDate: Date,
            finishDate: Date,
            employees:[], 
            projectId: null
        }
    },
    components:{
        findemployee,
       
    },
    props:{
        inputType: String
    },
    methods:{
        AddProject(){
            let options = {
            headers: { 'Content-Type': 'application/json' },
            url: 'https://localhost:44316/api/project/create',
            method: 'post',
            data: JSON.stringify({
                Name: this.name,
                Customer: this.customer,
                Performer: this.performer,
                LeadId: this.leadId,
                Priority: this.selected,
                StartDate: this.startDate,
                FinishDate: this.finishDate
                })
            };
            axios(options)
                    .then(response => {
                        this.projectId = response.data;
                        prompt("Проект успешно добавлен");
                        for(let i = 0; i < this.employees.length; i++){
                            let options = {
                                            headers: { 'Content-Type': 'application/json' },
                                            url: 'https://localhost:44316/api/projectsemployees/',
                                            method: 'post',
                                            data: JSON.stringify({
                                                ProjectId: this.projectId,
                                                EmployeeId: this.employees[i].id
                                                })
                                            };
                             axios(options)
                                .then(response => {

                                })
                                .catch(error => {
                                    console.log(error)
                                });
                        }
                    })
                    .catch(error => {
                        console.log(error);
                    });
            
        },
        getEmployees(data){
            this.employees = data;
            //console.log(this.employees);
        },
        getLead(data){
            this.leadId = data;
            //console.log(this.leadId);
        }

    },
    created(){
        this.today = new Date().toJSON().slice(0,10).replace(/-/g,'-');
        this.finishDate = this.today;
        this.startDate = this.today;
    }

   
}
</script>

<style>

</style>