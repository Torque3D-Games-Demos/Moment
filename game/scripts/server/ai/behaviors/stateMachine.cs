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

function AIStateMachine(%obj, %data, %priority)
{
   // Check that state datablock has the basic information.
   if(%data.stateName[0] $= "")
   {
      error("AIStateMachine datablock" SPC %data SPC "does not have a stateName[0]!");
      return;
   }
   // Priority defaults to 0.
   if(%priority $= "")
      %priority = 0.0;
   // Construct Behavior.
   %s = new Behavior() {
      class = AIStateMachine;
      object = %obj;
      data = %data;
      priority = %priority;
   };
   return %s;
}

function AIStateMachine::onAdd(%this)
{
   // Enter first state when we are created.
   %this.enterState(%this.data.stateName[0]);
}

function AIStateMachine::onEvent(%this, %event, %evtData)
{
   // Find transition for event in current state.
   %trans = %this.data.transition[%event][%this.state];
   if(%trans !$= "")
   {
      %this.exitState();
      %this.enterState(%trans);
   }
}

function AIStateMachine::enterState(%this, %state)
{
   // Find state by name.
   %i = 0;
   while(true)
   {
      if(%this.data.stateName[%i] $= "")
         break;
      else if(%this.data.stateName[%i] $= %state)
      {
         %this.state = %i;
         // Start up actions for this state.
         %j = 0;
         while(true)
         {
            %action = %this.data.stateAction[%i][%j];
            if(%action $= "")
               break;
            %this.object.brain.startAction(%action,
                  %this.priority,
                  %this.object,
                  %this, %i);
            %j++;
         }
         break;
      }
      %i++;
   }
}

function AIStateMachine::exitState(%this, %state)
{
   // Stop all actions associated with this state.
   %this.object.brain.stopActions("", "", %this, %this.state)
}

function AIStateMachine::onActionStopped(%this, %action, %data, %index, %status)
{
   // Ignore this for the moment.
}
