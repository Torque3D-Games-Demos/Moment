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

function AIPlayerBrain::onAdd(%this)
{
   // Utility function to provide standard AIPlayer resources.
   %this.resources[0] = "move";
   %this.resources[1] = "aim";
   %this.resources[2] = "voice";
}

//-----------------------------------------------------------------------------

new AIAction(AIMoveToAction) {
   resource = "move";
   allowWait = true;
   receiveEvents = true;
};

function AIMoveToAction::onStart(%this, %obj, %data, %resume)
{
   if(isObject(%data))
      %obj.setMoveDestination(%data.getPosition(), false);
   else
      %obj.setMoveDestination(%data, false);
}

function AIMoveToAction::onEvent(%this, %obj, %data, %event, %evtData)
{
   switch$(%event)
   {
      case "onReachDestination": return "Complete";
      case "onStuck":            return "Failed";
      default: return "Working";
   }
}

function AIMoveToAction::onEnd(%this, %obj, %status)
{
   %obj.stop();
}

//-----------------------------------------------------------------------------

new AIAction(AIWalkToAction : AIMoveToAction) {
   class = AIMoveToAction;
};

function AIWalkToAction::onStart(%this, %obj, %data, %resume)
{
   %obj.setMoveSpeed(0.4);
   Parent::onStart(%this, %obj, %data, %resume);
}

function AIWalkToAction::onEnd(%this, %obj, %status)
{
   %obj.setMoveSpeed(1);
   Parent::onEnd(%this, %obj, %status);
}

//-----------------------------------------------------------------------------

new AIAction(AISpeechAction) {
   resource = "voice";
   allowWait = true;
   receiveEvents = true;
};

function AISpeechAction::onStart(%this, %obj, %data, %resume)
{
   %obj.say(%data);
}

function AISpeechAction::onEvent(%this, %obj, %data, %event, %evtData)
{
   if(%event $= "onFinishedTalking")
      return "Complete";
   return "Working";
}

//-----------------------------------------------------------------------------

new AIAction(AIPainAction : AISpeechAction) {
   class = AISpeechAction;
   // No use playing pain at all if it's not immediate.
   allowWait = false;
};

//-----------------------------------------------------------------------------

new AIAction(AILookAtAction) {
   resource = "aim";
   allowWait = true;
   receiveEvents = true;
};

function AILookAtAction::onStart(%this, %obj, %data, %resume)
{
   if(isObject(%data))
      %obj.setAimObject(%data);
   else
      %obj.setAimLocation(%data);
}

function AILookAtAction::onEvent(%this, %obj, %data, %event, %evtData)
{
   switch$(%event)
   {
      case "onTargetExitLOS": return "Failed";
      default: return "Working";
   }
}

function AILookAtAction::onEnd(%this, %obj, %status)
{
   %obj.clearAim();
}

//-----------------------------------------------------------------------------

new AIAction(AIGlanceAtAction : AILookAtAction) {
   class = AILookAtAction;
   allowWait = false;
};

function AIGlanceAtAction::onStart(%this, %obj, %data, %resume)
{
   // HAXX0RS
   %obj.brain.schedule(1000, event, "beat", "");
   Parent::onStart(%this, %obj, %data, %resume);
}

function AIGlanceAtAction::onEvent(%this, %obj, %data, %event, %evtData)
{
   switch$(%event)
   {
      // This needs to be replaced with better timing logic.
      case "beat": return "Complete";
      default: return Parent::onEvent(%this, %obj, %data, %event, %evtData);
   }
}
