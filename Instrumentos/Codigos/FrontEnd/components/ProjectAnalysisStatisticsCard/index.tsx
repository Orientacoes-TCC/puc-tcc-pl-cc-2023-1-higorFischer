import React from "react"
import { ProjectAnalysis } from "../../models/models"

function ProjectCard(label: string, value: string | number) {
	return (<div style={{ border: "1px solid rgba(0,0,0,.3)", padding: "10px 20px", borderRadius: 4, textAlign: "center" }}>
		<div style={{ fontSize: "0.7rem", color: "rgba(0,0,0,.6)" }}>
			{label}
		</div>
		<div style={{ fontSize: "1.4rem", fontWeight: "bold" }}>
			{value}
		</div>
	</div>)
}

export function ProjectAnalysisStatisticsCard(props: { projectAnalysis: ProjectAnalysis }) {
	const { projectAnalysis } = props;

	const avgBadSmellPerTotalFiles = projectAnalysis?.totalBadSmells / projectAnalysis?.totalVerifiedFiles;
	const avgBadSmellPerFilesWithBadSmell = projectAnalysis?.totalBadSmells / projectAnalysis?.filesWithBadSmells;
	const percentFilesWithBadSmell = (projectAnalysis?.filesWithBadSmells / projectAnalysis?.totalBadSmells) * 100;

	return (
		<div style={{ width: "100%", background: "rgba(0,0,0,.05)", borderRadius: 8, display: "flex" }}>
			<div style={{ padding: 20, display: "flex", justifyContent: "space-around", width: "100%" }}>
				{ProjectCard("Files with Bad Smell", `${percentFilesWithBadSmell?.toFixed(2)}%`)}
				{ProjectCard("Average Bad Smell per file (Total)", avgBadSmellPerTotalFiles?.toLocaleString('pt-BR'))}
				{ProjectCard("Average Bad Smell per file (Only with Bad Smell)", avgBadSmellPerFilesWithBadSmell?.toLocaleString('pt-BR'))}
			</div>
		</div>
	)
}