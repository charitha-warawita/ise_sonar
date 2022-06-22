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
        CreateProject(project) {
            console.log('parameter: ' + JSON.stringify(project));
            console.log('store object: ' + JSON.stringify(this.project));
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
                "qualifications": this.LoadQualification(),
                "quotas": [],
                "subtotal": 0
            };
            this.project.projectTargetAudiences.push(ta)
        },
        CancelTargetAudience(ta) {
            var removeIndex = this.project.projectTargetAudiences.map(item => item.id).indexOf(ta.id);
            ~removeIndex && this.project.projectTargetAudiences.splice(removeIndex, 1);
        },
        LoadQualification() {
            return [
                {
                    "id": 1,
                    "name": "Country",
                    "text": "Which country do you live in?",
                    "categoryName": "Household",
                    "variables": [
                        {
                            "id": 1,
                            "name": "United Kingdowm"
                        },
                        {
                            "id": 2,
                            "name": "Sweden"
                        },
                        {
                            "id": 22,
                            "name": "United states of America"
                        }
                    ]
                },
                {
                    "id": 46,
                    "name": "Marital Status",
                    "text": "What is your marital status?",
                    "categoryName": "Household",
                    "variables": [
                        {
                            "id": 373,
                            "name": "Single"
                        },
                        {
                            "id": 374,
                            "name": "Domestic partnership"
                        },
                        {
                            "id": 375,
                            "name": "Married"
                        },
                        {
                            "id": 376,
                            "name": "Separated"
                        },
                        {
                            "id": 384,
                            "name": "Divorced"
                        },
                        {
                            "id": 385,
                            "name": "Widowed"
                        },
                        {
                            "id": 6654,
                            "name": "Prefer not to say"
                        },
                        {
                            "id": 10122,
                            "name": "Soon to be engaged"
                        },
                        {
                            "id": 10123,
                            "name": "Engaged"
                        }
                    ]
                },
                {
                    "id": 4,
                    "name": "Number of children",
                    "text": "How many children under the age of 18 live in your household?",
                    "categoryName": "Household",
                    "variables": [
                        {
                            "id": 4,
                            "name": "None"
                        },
                        {
                            "id": 5,
                            "name": "One"
                        },
                        {
                            "id": 6,
                            "name": "Two"
                        },
                        {
                            "id": 7,
                            "name": "Three"
                        },
                        {
                            "id": 8,
                            "name": "Four or more"
                        },
                        {
                            "id": 6628,
                            "name": "Prefer not to say"
                        }
                    ]
                },
                {
                    "id": 15,
                    "name": "Accommodation",
                    "text": "What is your accommodation situation?",
                    "categoryName": "Household",
                    "variables": [
                        {
                            "id": 3647,
                            "name": "Farm"
                        },
                        {
                            "id": 3648,
                            "name": "Living with my parents"
                        },
                        {
                            "id": 60,
                            "name": "Rented apartment"
                        },
                        {
                            "id": 61,
                            "name": "Owned apartment"
                        },
                        {
                            "id": 62,
                            "name": "Rented house"
                        },
                        {
                            "id": 63,
                            "name": "Owned house"
                        },
                        {
                            "id": 64,
                            "name": "Other"
                        },
                        {
                            "id": 6691,
                            "name": "Prefer not to say"
                        }
                    ]
                },
                {
                    "id": 16,
                    "name": "Access to car",
                    "text": "Do you have access to a car?",
                    "categoryName": "Automotive",
                    "variables": [
                        {
                            "id": 6599,
                            "name": "Prefer not to say"
                        },
                        {
                            "id": 65,
                            "name": "I own a car/cars"
                        },
                        {
                            "id": 66,
                            "name": "I have access to a car/cars"
                        },
                        {
                            "id": 67,
                            "name": "I lease/ have a company car"
                        },
                        {
                            "id": 68,
                            "name": "No, I don’t have access to a car/cars"
                        }
                    ]
                }];
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