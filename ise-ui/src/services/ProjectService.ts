//import type { Project } from "@/types/Project";
//import

import { subDays } from 'date-fns';
import Project from '@/model/Project';

export default class ProjectService {
	static GetProjectsAsync(): Project[] {
		const project = new Project();
		project.Id = 1111;
		project.Name = 'Project 1';
		project.MaconomyNumber = '123';
		project.Owner = 'John, Doe';
		project.CreationDate = subDays(new Date(), 5);
		project.LastActivity = subDays(new Date(), 2);

		const projects: Project[] = [project];

		return projects;
	}
}
