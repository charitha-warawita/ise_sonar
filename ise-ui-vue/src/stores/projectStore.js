import { defineStore } from "pinia";

export const useProjectStore = defineStore('project', {
    state: () => ({
        basicSettingDesc:'',
        totalCost: 0,
        project: {
            "id": "",
            "name": "",
            "reference": "",
            "userId": "",
            "lastUpdate": "",
            "startDate": "",
            "fieldingPeriod": 0,
            "status": "Draft",
            "linkToSurvey": "",
            "user": {
                "id": "",
                "name": "",
                "email": ""
            },
            "projectTargetAudiences": [],
            "cintResponseId": 0,
            "cintSelfLink": "",
            "cintCurrentCostLink": "",
            "cintTestingLink": ""
        }
    }),
    getters: {
    },
    actions: {
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
                "quotas": [],
                "subtotal": 0
            };
            this.project.projectTargetAudiences.push(ta)
        },
        AddQualificationElement(quals) {
            console.log("Came to add qualifications")
            var qual = {
                "id": "",
                "name": "",
                "condition":"",
                "order": 0,
                "isActive": true
            };
            quals.push(qual)
        },
        AddQuotaElement(quots) {
            var quot = {
                "id": "",
                "name": "",
                "condition": "",
                "limit": 0,
                "limitType": "",
                "isActive": true
            };
            quots.push(quot);
        },
        CalculateCharges() {
            if(this.project.projectTargetAudiences !== undefined)
            {
                console.log("came in running CPI cost");
                this.totalCost = 0;
                for(var i =0; i < this.project.projectTargetAudiences.length; i++) {
                if(this.project.projectTargetAudiences[i].wantedCompletes > 0 && this.project.projectTargetAudiences[i].estimatedIR > 0 && this.project.projectTargetAudiences[i].estimatedLOI > 0) {
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