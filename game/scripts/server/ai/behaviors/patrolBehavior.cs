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

function createAIPatrolBehavior(%obj, %path, %priority)
{
   // Default priority to NOTHING.
   if(%priority $= "")
      %priority = 0.0;
   // Construct a behavior that loops infinitely.
   %b = new Behavior() {
      class = AIPatrolBehavior;
      object = %obj;
      path = %path;
      pathNode = 0;
      priority = %priority;
   };
   return %b;
}

function AIPatrolBehavior::onAdd(%this)
{
   // Start moving to the first path node.
   %this.advance();
}

function AIPatrolBehavior::advance(%this)
{
   %this.object.brain.startAction(AIWalkToAction,
      %this.priority,
      %this.path.getObject(%this.pathNode).getPosition(),
      %this, 0);
}

function AIPatrolBehavior::onActionStopped(%this, %action, %data, %index, %status)
{
   if(!isObject(%this.object))
      return;
   // Ignore failures and waiting.
   if(%status !$= "Complete")
      return;
   // Select next node.
   %this.pathNode++;
   if(%this.pathNode >= %this.path.getCount())
      %this.pathNode = 0;
   // Move to the node.
   %this.advance();
}
