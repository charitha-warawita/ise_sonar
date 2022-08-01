import type { Project } from "@/types/Project";

import { subDays, format } from 'date-fns';

export default class ProjectService {

    static GetProjectsAsync() : Project[] {
        // let projects = new Array<Project>;
        let projects : Project[] = [{
                Id: 1111,
                Name: "Something",
                MaconomyNumber: "123",
                Owner: "Me",
                CreationDate: subDays(new Date(), 5),
                LastActivity: subDays(new Date(), 5),    
            },
            {
                Id: 2222,
                Name: "Something",
                MaconomyNumber: "456",
                Owner: "Me",
                CreationDate: subDays(new Date(), 10),
                LastActivity: subDays(new Date(), 2),    
            },
            {
                Id: 3333,
                Name: "Something",
                MaconomyNumber: "789",
                Owner: "Me",
                CreationDate: subDays(new Date(), 20),
                LastActivity: subDays(new Date(), 15),    
            }
        ];

        return projects;
    }

}