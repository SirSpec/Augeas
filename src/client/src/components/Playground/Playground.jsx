import React, { useState, useEffect, useRef } from 'react';

import PlaygroundService from './PlaygroundService'

const Playground = () => {
    const playgroundViewRef = useRef(null);
    const canvasRef = useRef(null);

    useEffect(() => {
        var playgroundService = new PlaygroundService(playgroundViewRef, canvasRef);
        playgroundService.run();
    }, []);

    return (
        <div id="mapView" ref={playgroundViewRef}>
            <canvas ref={canvasRef} />
        </div>
    );
};

export default Playground;