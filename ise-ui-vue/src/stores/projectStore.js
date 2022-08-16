import { defineStore } from "pinia";
import useVuelidate from '@vuelidate/core'
import { required } from '@vuelidate/validators'
export const useProjectStore = defineStore('project', {
    state: () => ({
        basicSettingDesc:'',
        totalCost: 0,
        project: {
            "tempId": 0,
            "name": "",
            "reference": "",
            "startDate": "",
            "fieldingPeriod": 0,
            "status": 0,
            "categories": [],
            "user": {
                "id": 0,
                "name": "",
                "email": ""
            },
            "errors":[],
            "targetAudiences": []
        },
        getCintRequestLoading: false,
        getCintRequestError: null,
        cintRequests: null,
        saveProjectLoading: false,
        saveProjectError: null,
        categories:[],
        loading: false,
        error: null,

    }),
    rules: {
        name: { required },
        reference: { required }
    },
    actions: {
        async ProjectAPIValidated(project) {
            this.getCintRequestLoading = true;
            var iseUrl = import.meta.env.VITE_ISE_API_URL;
            var cintRequestPath = import.meta.env.VITE_ISE_API_CINTREQUEST;
            const settings = { 
                method: 'POST',
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(project)
            };
            try {
                var cintReqs = await fetch((iseUrl + cintRequestPath), settings)
                .then((response) => response.json());
                if(cintReqs!== null) {
                    if(!(cintReqs.find(x => x.ValidationResult === false))) {
                        this.cintRequests = cintReqs;
                        return true;
                    }
                    else
                        this.getCintRequestError = cintReqs.errors;
                }
            } catch (error) {
                this.getCintRequestError = [];
                this.getCintRequestError.push(error);
            } finally {
                this.getCintRequestLoading = false;
            }
            return false;
        },
        async CreateProject(project) {
            // project.lastUpdate = new Date();

            this.saveProjectLoading = true;
            var iseUrl = import.meta.env.VITE_ISE_API_URL;
            var saveProjectPath = import.meta.env.VITE_ISE_API_SAVEPROJECT;
            const settings = { 
                method: 'POST',
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(project)
            }
            try {
                var savedProject = await fetch((iseUrl + saveProjectPath), settings)
                .then((response) => response.json());
                if(savedProject.id === undefined)
                    throw savedProject;
                project.id = savedProject.id;
                project.lastUpdate = savedProject.lastUpdate;
            } catch (error) {
                this.saveProjectError = error;
                project.errors.push(error);
            } finally {
                this.saveProjectLoading = false;
            }
        },
        AddTargetAudienceElement() {  
            var id = this.project.targetAudiences.length;
            var ta = {
                "tempId": id+1,
                "name": "",
                "audienceNumber":0,
                "estimatedIR": 0,
                "estimatedLOI": 0,
                "costPerInterview": 0,
                "cptg": 0,
                "limit": 0,
                "testingUrl":"",
                "liveUrl":"",
                "qualifications": [],
                "quotas": [],
                "subtotal": 0,
                "errors": []
            };
            this.project.targetAudiences.push(ta)
        },
        CancelTargetAudience(ta) {
            var removeIndex = this.project.targetAudiences.map(item => item.tempId).indexOf(ta.tempId);
            ~removeIndex && this.project.targetAudiences.splice(removeIndex, 1);
        },
        CalculateCharges(event) {
            if(this.project.targetAudiences !== undefined)
            {
                console.log("came in running CPI cost");
                console.log("came in running CPI cost");
                this.totalCost = 0;
                for(var i =0; i < this.project.targetAudiences.length; i++) {
                if(this.project.targetAudiences[i].limit > 0 || this.project.targetAudiences[i].estimatedIR > 0 || this.project.targetAudiences[i].estimatedLOI > 0) {
                    var ir = this.project.targetAudiences[i].estimatedIR; var loi = this.project.targetAudiences[i].estimatedLOI
                    if(ir>= 75 && ir <=100 && loi >0 && loi <=5)
                        this.project.targetAudiences[i].costPerInterview = 2;
                    else if(ir>= 50 && ir <=74 && loi >0 && loi <=5)
                        this.project.targetAudiences[i].costPerInterview = 2.5;
                    else if(ir>= 35 && ir <=49 && loi >0 && loi <=5)
                        this.project.targetAudiences[i].costPerInterview = 3;
                    else if(ir>= 11 && ir <=34 && loi >0 && loi <=5)
                        this.project.targetAudiences[i].costPerInterview = 4;
                    else if(ir>= 1 && ir <=10 && loi >0 && loi <=5)
                        this.project.targetAudiences[i].costPerInterview = 7;
                    else if(ir>= 75 && ir <=100 && loi >5 && loi <=10)
                        this.project.targetAudiences[i].costPerInterview = 2.5;
                    else if(ir>= 50 && ir <=74 && loi >5 && loi <=10)
                        this.project.targetAudiences[i].costPerInterview = 3;
                    else if(ir>= 35 && ir <=49 && loi >5 && loi <=10)
                        this.project.targetAudiences[i].costPerInterview = 4;
                    else if(ir>= 11 && ir <=34 && loi >5 && loi <=10)
                        this.project.targetAudiences[i].costPerInterview = 7;
                    else if(ir>= 1 && ir <=10 && loi >5 && loi <=10)
                        this.project.targetAudiences[i].costPerInterview = 8;
                    else if(ir>= 75 && ir <=100 && loi >10 && loi <=15)
                        this.project.targetAudiences[i].costPerInterview = 3;
                    else if(ir>= 50 && ir <=74 && loi >10 && loi <=15)
                        this.project.targetAudiences[i].costPerInterview = 3.5;
                    else if(ir>= 35 && ir <=49 && loi >10 && loi <=15)
                        this.project.targetAudiences[i].costPerInterview = 4.5;
                    else if(ir>= 11 && ir <=34 && loi >10  && loi <=15)
                        this.project.targetAudiences[i].costPerInterview = 7.5;
                    else if(ir>= 1 && ir <=10 && loi >10 && loi <=15)
                        this.project.targetAudiences[i].costPerInterview = 8.5;
                    else 
                        this.project.targetAudiences[i].costPerInterview = 10;
                    var subT = this.project.targetAudiences[i].costPerInterview * this.project.targetAudiences[i].limit;
                    this.project.targetAudiences[i].cptg = subT;
                    this.project.targetAudiences[i].subtotal = subT;
                    this.totalCost = this.totalCost + subT;
                }
            }
            }
        }
    }
})