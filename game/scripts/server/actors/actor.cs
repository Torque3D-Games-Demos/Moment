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

function Actor::onAdd(%this, %obj)
{
   if(%obj.getClassName() $= "AIPlayer")
   {
      if(%this.sensorData !$= "")
      {
         %obj.senses = new Sensor() {
            datablock = %this.sensorData;
            object = %obj;
            callbackObject = %this;
         };
      }
   }
}

function Actor::onRemove(%this, %obj)
{
   if(isObject(%obj.brain))
      %obj.brain.delete();
   if(isObject(%obj.senses))
      %obj.senses.delete();
}

//-----------------------------------------------------------------------------
// Mounting and control objects

function Actor::onMount(%this, %obj, %mount)
{
   if(%obj.getControllingClient())
      commandToClient(%obj.getControllingClient(), 'mountVehicle');
}

function Actor::onUnmount(%this, %obj, %mount)
{
   if(%obj.getControllingClient())
      commandToClient(%obj.getControllingClient(), 'unmountVehicle');
}

//-----------------------------------------------------------------------------
// AI events

function Actor::onReachDestination(%this, %obj)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onReachDestination");
}

function Actor::onStuck(%this, %obj)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onStuck");
}

function Actor::onTargetEnterLOS(%this, %obj)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onTargetEnterLOS");
}

function Actor::onTargetExitLOS(%this, %obj)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onTargetExitLOS");
}

function Actor::onFinishedTalking(%this, %obj)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onFinishedTalking");
}

function Actor::onDamage(%this, %obj, %delta)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onDamage", %delta);
}

function Actor::onNewContact(%this, %obj, %contact, %vis)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onNewContact", %contact SPC %vis);
}

function Actor::onContactSighted(%this, %obj, %contact, %vis)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onContactSighted", %contact SPC %vis);
}

function Actor::onContactLost(%this, %obj, %contact, %vis)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onContactLost", %contact SPC %vis);
}

function Actor::onContactVisibilityChanged(%this, %obj, %contact, %vis)
{
   if(isObject(%obj.brain))
      %obj.brain.onEvent("onContactVisibilityChanged", %contact SPC %vis);
}
