import { z } from "zod";

export enum LinkKeyEnum {
	EntryUrl = "entryUrl",
	TestCompleteSurvey = "testCompleteSurvey",
	CurrentConst = "currentCost",
	Respondents = "respondents",
	Self = "self",
	TestCreateRespondents = "testCreateRespondents",
	TestCreateStartedRespondents = "testCreateStartedRespondents",
	Testing = "testing",
	TestQualityHalt = "testQualityHalt",
	TestRepriceHalt = "testRepriceHalt",
}

export const LinkKeySchema = z.nativeEnum(LinkKeyEnum);
export type LinkKey = z.infer<typeof LinkKeySchema>;
