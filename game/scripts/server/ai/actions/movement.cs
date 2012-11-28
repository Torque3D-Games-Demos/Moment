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

new AIAction(AICircleStrafeAction) {
   resource = "move";
   allowWait = true;
   receiveEvents = true;
};

function AICircleStrafeAction::onStart(%this, %obj, %data, %resume)
{
   %this.onEvent(%obj, %data, "onReachDestination");
}

function AICircleStrafeAction::onEvent(%this, %obj, %data, %event, %evtData)
{
   switch$(%event)
   {
      case "onReachDestination":
         // Get object we're strafing around
         %target = getWord(%data, 0);
         // Relative normal vector in 2D
         %rel = VectorSub(%obj.getPosition(), %target.getPosition());
         %rel = setWord(%rel, 2, 0);
         %rel = VectorNormalize(%rel);
         // Angle from north is acos(rel.y)
         %ang = mAcos(getWord(%rel, 1));
         // Increment angle and reconstruct target location
         %ang += mDegToRad(360/20);
         %rel = setWord(%rel, 0, mSin(%ang));
         %rel = setWord(%rel, 1, mCos(%ang));
         %rel = VectorScale(%rel, getWord(%data, %data, 1));
         // Move to!
         %obj.setMoveDestination(VectorAdd(%target.getPosition(), %rel), false);
   }
   return "Working";
}

function AICircleStrafeAction::onEnd(%this, %obj, %status)
{
   %obj.stop();
}
