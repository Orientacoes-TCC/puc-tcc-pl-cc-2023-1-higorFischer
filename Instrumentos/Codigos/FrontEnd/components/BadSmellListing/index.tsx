import React, { useEffect, useState } from 'react';
import { CodeAnalysis } from "../../models/models";

export function BadSmellListing(props: { codeAnalysis: CodeAnalysis[] }) {
	const { codeAnalysis } = props

	const [chart, setChart] = useState<any>([]);

	useEffect(() => {
		var c = codeAnalysis?.flatMap(a => a.badSmells);
		var objects = c?.reduce((a: any, b) => {
			if (!a[b.name])
				a[b.name] = [];
			a[b.name].push(b);
			return a;
		}, {}) as any;

		console.log(objects)

		if (objects)
			setChart(objects)
	}, [codeAnalysis]);

	return (
		<div style={{ width: "100%" }}>
			{Object.keys(chart).map((c) => {
				const items = chart[c];
				return (
					<div key={c} style={{ background: "rgba(0,0,0,.05)", borderRadius: 8, width: "100%", marginBottom: 10 }}>
						<div style={{ padding: 10 }}>
							<div style={{ marginBottom: 10 }}>
								{c}
							</div>
							<div style={{ height: "100px", overflow: "auto" }}>
								{items?.map((item: any) => {
									const { Name, ...otherParameters } = item.parameters;

									return <div style={{ fontSize: "0.7rem" }}>
										{item?.parameters?.Name} ({JSON.stringify(otherParameters)})
									</div>
								})}
							</div>
						</div>
					</div>
				)

			})}
		</div>
	)
}