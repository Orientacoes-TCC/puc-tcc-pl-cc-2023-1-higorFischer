import { useEffect, useState } from "react";
import { BadSmell, CodeAnalysis } from "../../models/models"
import { ResponsiveContainer, Cell, BarChart, Bar, YAxis, XAxis } from "recharts";

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#00C4EE'];

export function CodeAnalysisChart(props: { codeAnalysis: CodeAnalysis[] }) {
	const { codeAnalysis } = props;

	const [chart, setChart] = useState<any>([]);

	useEffect(() => {
		var c = codeAnalysis?.flatMap(a => a.badSmells);
		var objects = c?.reduce((a: any, b) => {
			if (!a[b.name])
				a[b.name] = [];
			a[b.name].push(b);
			return a;
		}, {}) as any;

		if (objects)
			setChart(Object.keys(objects).map(c => {
				return { name: c, amount: objects[c].length };
			}))

	}, [codeAnalysis]);

	return (
		<div style={{ background: "rgba(0,0,0,.05)", borderRadius: 8, padding: "0 50px", width: "100%", paddingTop: 10 }}>
			<ResponsiveContainer width="100%" height="100%" >
				<BarChart data={chart} margin={{ top: 20 }}>
					<XAxis dataKey="name" fontSize="0.6rem" />
					<YAxis label="Lines" hide />
					<Bar
						dataKey="amount"
						barSize={50}
						label={{ position: 'top' }}
					>
						{chart?.map((entry, index) => (
							<Cell key={`cell-${index}`} fill={COLORS[index % 20]} />
						))}
					</Bar>
				</BarChart>
			</ResponsiveContainer>
		</div>
	)
}