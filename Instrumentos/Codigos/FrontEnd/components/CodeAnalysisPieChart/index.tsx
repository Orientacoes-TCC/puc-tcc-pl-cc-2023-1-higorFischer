import React, { useEffect, useState } from "react";
import { BadSmell, CodeAnalysis } from "../../models/models"
import { ResponsiveContainer, BarChart, Bar, YAxis, XAxis, Cell, Pie, PieChart, Legend } from "recharts";


const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#00C4EE'];

const RADIAN = Math.PI / 180;
const renderCustomizedLabel = ({ cx, cy, midAngle, innerRadius, outerRadius, percent, index }) => {
	const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
	const x = cx + radius * Math.cos(-midAngle * RADIAN);
	const y = cy + radius * Math.sin(-midAngle * RADIAN);

	return (
		<text x={x + (x > cx ? -2.5 : 5)} y={y + (x > cx ? 5 : 0)} fill="white" textAnchor={x > cx ? 'start' : 'end'} dominantBaseline="central" fontSize="0.6rem">
			{`${(percent * 100).toFixed(0)}%`}
		</text>
	);
};


export function CodeAnalysisPieChart(props: { codeAnalysis: CodeAnalysis[] }) {
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
				<PieChart width={400} height={400} >
					<Pie
						data={chart}
						cx="50%"
						cy="50%"
						labelLine={false}
						label={renderCustomizedLabel}
						outerRadius={80}
						fill="#8884d8"
						dataKey="amount"
					>
						{chart?.map((entry, index) => (
							<Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} fontSize="0.7rem" />
						))}
					</Pie>
				</PieChart>
			</ResponsiveContainer>
		</div>
	)
}