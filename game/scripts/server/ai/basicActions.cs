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
      %obj.setMoveDestination(%data.getPosition());
   else
      %obj.setMoveDestination(%data);
}

function AIMoveToAction::onEvent(%this, %obj, %data, %event)
{
   switch$(%event)
   {
      case "onReachDestination": return "Complete";
      case "onStuck":            return "Failed";
   }
   return "Working";
}

new AIAction(AISpeechAction) {
   resource = "voice";
   allowWait = true;
   receiveEvents = true;
};

function AISpeechAction::onStart(%this, %obj, %data, %resume)
{
   %obj.say(%data);
}

function AISpeechAction::onEvent(%this, %obj, %data, %event)
{
   if(%event $= "onFinishedTalking")
      return "Complete";
   return "Working";
}

new AIAction(AIPainAction : AISpeechAction) {
   class = AISpeechAction;
   // No use playing pain at all if it's not immediate.
   allowWait = false;
};
