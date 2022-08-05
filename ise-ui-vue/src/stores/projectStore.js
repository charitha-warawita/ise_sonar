import { defineStore } from "pinia";
export const useProjectStore = defineStore('project', {
    state: () => ({
        basicSettingDesc:'',
        totalCost: 0,
        project: {
            "id": 0,
            "name": "",
            "reference": "",
            "lastUpdate": "",
            "startDate": "",
            "fieldingPeriod": 0,
            "status": 0,
            "testingUrl": "",
            "liveUrl": "",
            "categories": [],
            "user": {
                "id": 0,
                "name": "",
                "email": ""
            },
            "projectTargetAudiences": []
        },
        saveProjectLoading: false,
        saveProjectError: null,
        categories:[],
        loading: false,
        error: null,

    }),
    getters: {
    },
    actions: {
        async CreateProject(project) {
            project.tempId = project.id;
            project.lastUpdate = new Date();
            delete project.id;

            for(var i = 0; i < project.projectTargetAudiences.length; i++) {
                project.projectTargetAudiences[i].tempId = project.projectTargetAudiences[i].id ;
                delete project.projectTargetAudiences[i].id;
                for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++) {
                    project.projectTargetAudiences[i].qualifications[j].tempId = project.projectTargetAudiences[i].qualifications[j].id;
                    delete project.projectTargetAudiences[i].qualifications[j].id;
                }
            }

            project.targetAudiences = project.projectTargetAudiences 
            delete project.projectTargetAudiences;

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
                project.id = savedProject.id;
                //project.lastUpdate = savedProject.lastUpdate;
            } catch (error) {
                this.saveProjectError = error;
            } finally {
                this.saveProjectLoading = false;
            }
        },
        AddTargetAudienceElement() {  
            var id = this.project.projectTargetAudiences.length;
            var ta = {
                "id": id+1,
                "name": "",
                "audienceNumber":0,
                "estimatedIR": 0,
                "estimatedLOI": 0,
                "costPerInterview": 0,
                "cptg": 0,
                "wantedCompletes": 0,
                "qualifications": [],
                "quota": [],
                "quotas": [],
                "subtotal": 0
            };
            this.project.projectTargetAudiences.push(ta)
        },
        CancelTargetAudience(ta) {
            var removeIndex = this.project.projectTargetAudiences.map(item => item.id).indexOf(ta.id);
            ~removeIndex && this.project.projectTargetAudiences.splice(removeIndex, 1);
        },
        CalculateCharges(event) {
            if(this.project.projectTargetAudiences !== undefined)
            {
                console.log("came in running CPI cost");
                console.log("came in running CPI cost");
                this.totalCost = 0;
                for(var i =0; i < this.project.projectTargetAudiences.length; i++) {
                if(this.project.projectTargetAudiences[i].wantedCompletes > 0 || this.project.projectTargetAudiences[i].estimatedIR > 0 || this.project.projectTargetAudiences[i].estimatedLOI > 0) {
                    var ir = this.project.projectTargetAudiences[i].estimatedIR; var loi = this.project.projectTargetAudiences[i].estimatedLOI
                    if(ir>= 75 && ir <=100 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 2;
                    else if(ir>= 50 && ir <=74 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 2.5;
                    else if(ir>= 35 && ir <=49 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 3;
                    else if(ir>= 11 && ir <=34 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 4;
                    else if(ir>= 1 && ir <=10 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 7;
                    else if(ir>= 75 && ir <=100 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 2.5;
                    else if(ir>= 50 && ir <=74 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 3;
                    else if(ir>= 35 && ir <=49 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 4;
                    else if(ir>= 11 && ir <=34 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 7;
                    else if(ir>= 1 && ir <=10 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 8;
                    else if(ir>= 75 && ir <=100 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 3;
                    else if(ir>= 50 && ir <=74 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 3.5;
                    else if(ir>= 35 && ir <=49 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 4.5;
                    else if(ir>= 11 && ir <=34 && loi >10  && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 7.5;
                    else if(ir>= 1 && ir <=10 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 8.5;
                    else 
                        this.project.projectTargetAudiences[i].costPerInterview = 10;
                    var subT = this.project.projectTargetAudiences[i].costPerInterview * this.project.projectTargetAudiences[i].wantedCompletes;
                    this.project.projectTargetAudiences[i].cptg = subT;
                    this.project.projectTargetAudiences[i].subtotal = subT;
                    this.totalCost = this.totalCost + subT;
                }
            }
            }
        }
    }
})