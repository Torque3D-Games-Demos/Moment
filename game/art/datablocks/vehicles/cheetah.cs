//-----------------------------------------------------------------------------
// Copyright (c) 2012 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

datablock ParticleData(CheetahTireParticle)
{
   textureName          = "art/particles/dustParticle";
   dragCoefficient      = 1.99902;
   gravityCoefficient   = -0.100122;
   inheritedVelFactor   = 0.0998043;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 400;
   colors[0]            = "0.456693 0.354331 0.259843 1";
   colors[1]            = "0.456693 0.456693 0.354331 0";
   sizes[0]             = 0.997986;
   sizes[1]             = 3.99805;
   sizes[2]             = 1.0;
   sizes[3]             = 1.0;
   times[0]             = 0.0;
   times[1]             = 1;
   times[2]             = 1;
   times[3]             = 1;
};

datablock ParticleEmitterData(CheetahTireEmitter)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 10;
   ejectionVelocity = 15;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "CheetahTireParticle";
   blendStyle = "ADDITIVE";
};

//-----------------------------------------------------------------------------
// Information extacted from the shape.
//
// Wheel Sequences
//    spring#        Wheel spring motion: time 0 = wheel fully extended,
//                   the hub must be displaced, but not directly animated
//                   as it will be rotated in code.
// Other Sequences
//    steering       Wheel steering: time 0 = full right, 0.5 = center
//    breakLight     Break light, time 0 = off, 1 = breaking
//
// Wheel Nodes
//    hub#           Wheel hub, the hub must be in it's upper position
//                   from which the springs are mounted.
//
// The steering and animation sequences are optional.
// The center of the shape acts as the center of mass for the car.

//-----------------------------------------------------------------------------
datablock WheeledVehicleTire(CheetahCarTire)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "art/shapes/cheetah/wheel.dae";
   staticFriction = 4.2;
   kineticFriction = 1;

   // Spring that generates lateral tire forces
   lateralForce = 18000;
   lateralDamping = 6000;
   lateralRelaxation = 1;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 18000;
   longitudinalDamping = 4000;
   longitudinalRelaxation = 1;
   radius = 0.6;
};

datablock WheeledVehicleTire(CheetahCarTireRear)
{
   // Tires act as springs and generate lateral and longitudinal
   // forces to move the vehicle. These distortion/spring forces
   // are what convert wheel angular velocity into forces that
   // act on the rigid body.
   shapeFile = "art/shapes/cheetah/wheelBack.dae";
   staticFriction = 7.2;
   kineticFriction = 1;

   // Spring that generates lateral tire forces
   lateralForce = 19000;
   lateralDamping = 6000;
   lateralRelaxation = 1;

   // Spring that generates longitudinal tire forces
   longitudinalForce = 18000;
   longitudinalDamping = 4000;
   longitudinalRelaxation = 1;
   radius = 0.85;
};

datablock WheeledVehicleSpring(CheetahCarSpring)
{
   // Wheel suspension properties
   length = 0.5;             // Suspension travel
   force = 2800;              // Spring force
   damping = 3600;             // Spring damping
   antiSwayForce = 3;         // Lateral anti-sway force
};

datablock WheeledVehicleData(Cheetah)
{
   category = "Vehicles";
   shapeFile = "art/shapes/cheetah/Cheetah_Body.dae";
   emap = 1;

   mountPose[0] = sitting;
   numSeats = 4;
   driverSeat = 0;

   useEyePoint = true;  // Use the vehicle's camera node rather than the player's

   maxSteeringAngle = 0.585;  // Maximum steering angle, should match animation

   // 3rd person camera settings
   cameraRoll = false;        // Roll the camera with the vehicle
   cameraMaxDist = 7.8;       // Far distance from vehicle
   cameraOffset = 1.0;        // Vertical offset from camera mount point
   cameraLag = "0.3";           // Velocity lag of camera
   cameraDecay = 1.25;        // Decay per sec. rate of velocity lag

   // Rigid Body
   mass = 400;
   massCenter = "0 0.5 0";    // Center of mass for rigid body
   massBox = "0 0 0";         // Size of box used for moment of inertia,
                              // if zero it defaults to object bounding box
   drag = 0.6;                // Drag coefficient
   bodyFriction = 0.6;
   bodyRestitution = 0.4;
   minImpactSpeed = 5;        // Impacts over this invoke the script callback
   softImpactSpeed = 5;       // Play SoftImpact Sound
   hardImpactSpeed = 15;      // Play HardImpact Sound
   integration = 8;           // Physics integration: TickSec/Rate
   collisionTol = 0.1;        // Collision distance tolerance
   contactTol = 0.4;          // Contact velocity tolerance

   // Engine
   engineTorque = 4300;       // Engine power
   engineBrake = 5000;         // Braking when throttle is 0
   brakeTorque = 10000;        // When brakes are applied
   maxWheelSpeed = 50;        // Engine scale by current speed / max speed

   // Wheels
   tireData[0] = CheetahCarTire;
   springData[0] = CheetahCarSpring;
   wheelPowered[0] = true;
   wheelSteering[0] = 1;

   tireData[1] = CheetahCarTire;
   springData[1] = CheetahCarSpring;
   wheelPowered[1] = true;
   wheelSteering[1] = 1;

   tireData[2] = CheetahCarTireRear;
   springData[2] = CheetahCarSpring;
   wheelPowered[2] = true;
   wheelSteering[2] = -1;

   tireData[3] = CheetahCarTireRear;
   springData[3] = CheetahCarSpring;
   wheelPowered[3] = true;
   wheelSteering[3] = -1;

   // Energy
   maxEnergy = 100;
   jetForce = 3000;
   minJetEnergy = 30;
   jetEnergyDrain = 2;

   // Sounds
   engineSound = cheetahEngine;
   //squealSound = cheetahSqueal;
   softImpactSound = softImpact;
   hardImpactSound = hardImpact;

   // Dynamic fields accessed via script
   nameTag = 'Cheetah';
   maxDismountSpeed = 10;
   maxMountSpeed = 5;
   mountPose0 = "sitting";
   tireEmitter = "CheetahTireEmitter";
   dustEmitter = "CheetahTireEmitter";
   dustHeight = 1;

   // Mount slots
   turretSlot = 1;
   rightBrakeSlot = 2;
   leftBrakeSlot = 3;
};
