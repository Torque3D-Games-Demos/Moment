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
