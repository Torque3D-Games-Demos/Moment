//-----------------------------------------------------------------------------
// Copyright (c) 2012 Daniel Buckmaster
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

$n = -1;
datablock ShapeBaseImageData(M16Image)
{
   shapeFile = "art/shapes/weapons/m16/m16.dts";
   
   mountPoint = 0;
   
   stateName[$n++] = "ready";
   stateTransitionOnNoAmmo[$n] = "dry";
   stateTransitionOnTriggerDown[$n] = "fire";
   
   stateName[$n++] = "dry";
   stateTransitionOnLoaded[$n] = "ready";
   stateTransitionOnTriggerDown[$n] = "dryFire";
   
   stateName[$n++] = "dryFire";
   stateTransitionOnTriggerUp[$n] = "dry";
   
   stateName[$n++] = "fire";
   stateScript[$n] = "onFire";
   stateFire[$n] = true;
   stateTransitionOnTriggerUp[$n] = "ready";
};

datablock ItemData(M16Item)
{
   shapeFile = "art/shapes/weapons/m16/m16.dts";
   
   mass = 10;
   friction = 0.9;
   elasticity = 0.1;
   density = 50;
   
   name = "M16 rifle";
};
