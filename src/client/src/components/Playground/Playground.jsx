import React, { useState, useEffect, useRef } from 'react';

import PlaygroundService from './PlaygroundService'

const Playground = () => {
	const playgroundViewRef = useRef(null);
	const canvasRef = useRef(null);

	useEffect(() => {
		var playgroundService = new PlaygroundService(
			playgroundViewRef,
			canvasRef,
			window.innerWidth * 0.98,
			window.innerHeight * 0.97);

		playgroundService.initialize();
	}, []);

	return null;
};

export default Playground;