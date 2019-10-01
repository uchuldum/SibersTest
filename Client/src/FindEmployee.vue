<template>
    <div>
        <input placeholder="Введите для поиска" @click="myValue = ''" v-model="myValue" type="text" />
        <select @change="SelectEmployee($event)">
            <option value="" selected disabled>Выберите из списка</option>
            <option
                 v-for = "employee in employees" :id="employee.employeeId" :key="employee.employeeId"
            >
            {{employee.surName}} {{employee.name}} {{employee.patronymic}}
            </option>
        </select>
        <ul>
            <li
                v-for = "selectedEmp in selectedEmployees"
            >
            {{selectedEmp.name}}
            <span :id = selectedEmp.id @click="deleteEmployee($event)">Удалить</span>
            </li>
        </ul>
    </div>
</template>

<script>

import axios from 'axios'

export default {
    data(){
        return{
            myValue: '',
            employees: [],
            selectedEmployees:[],
            selectedLeadId: null,
            selectedEmployee: {
                name: "",
                id:""
            },
            
         
        }
    },
    props: {
        inputType: String,
    },
    components:{
        
    },
    methods:{
        FindEmployee(_name){
            let options = {
            headers: { 'Content-Type': 'application/json' },
            url: 'https://localhost:44316/api/employee/getname',
            method: 'post',
            data: JSON.stringify({name: _name})
            }
            axios(options)
                    .then(response => {
                        this.employees = response.data;
                    })
                    .catch(error => {
                        console.log(error);
                    })
        },
        SelectEmployee(e){
            //this.selectedEmployees.push(employee);
            //e.target.options
            this.selectedLeadId = e.target.options[e.target.options.selectedIndex].id;
            this.myValue = e.target.value;
            e.target.value = "";
            //console.log(this.selectedLeadId);
            if(this.inputType == "checkbox"){
                let exist = false;
                for(let i = 0; i < this.selectedEmployees.length; i++)
                {
                    if(this.selectedEmployees[i].id == this.selectedLeadId){
                        exist = true;
                        break;
                    }
                }
                if(exist == false){
                    this.getEmployeeById(this.selectedLeadId);
                    this.selectedEmployees.push(Object.assign({},this.selectedEmployee));
                    
                    this.$emit('employees',this.selectedEmployees)
                }
            }
            else{
                this.$emit('lead', this.selectedLeadId);
            }
               
        },
        getEmployeeById(id){
            for(let i = 0; i < this.employees.length; i++){
                if(this.employees[i].employeeId == id)
                {
                    this.selectedEmployee.name = this.employees[i].surName + " " + this.employees[i].name + " " + this.employees[i].patronymic;
                    this.selectedEmployee.id = id;
                    break;
                }
            }
        },
        deleteEmployee(e){
           
            for(let i = 0; i < this.selectedEmployees.length; i++){
                if(this.selectedEmployees[i].id == e.target.id){
                    this.selectedEmployees.splice(i,1);
                    break;
                }
            }
            this.$emit('employees',this.selectedEmployees)
        }
    },
    watch:{
        'myValue': function(val){
            this.FindEmployee(val.toLowerCase().trim());
           
        }
    },
    created(){
        this.FindEmployee();
    }
    
}
</script>

<style>
    span{
        border: 1px solid #ccc;
        background-color: #f0f0f0;
    }
    span:hover{
        background-color: red;
        cursor: pointer;
    }
    li {
    list-style-type: none; 
    }
    ul {
    margin-left: 2px; /* Отступ слева в браузере IE и Opera */
    padding-left: 2px; /* Отступ слева в браузере Firefox, Safari, Chrome */
   }
</style>