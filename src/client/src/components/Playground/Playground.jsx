import React, { useState, useEffect, useRef } from 'react';

import PlaygroundService from './PlaygroundService'

const Playground = () => {
    const playgroundViewRef = useRef(null);
    const canvasRef = useRef(null);

    useEffect(() => {
        var playgroundService = new PlaygroundService(
            playgroundViewRef,
            canvasRef,
            window.innerWidth * 0.8,
            window.innerHeight * 0.9);

        playgroundService.initialize();
    }, []);

    return null;
};

export default Playground;