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

// Sanitise client ghost objects.
function GameConnection::resolveObject(%this, %ghostId, %classes)
{
   %obj = %this.resolveObjectFromGhostIndex(%ghostId);
   if(!isObject(%obj))
      return -1;
   if(getWordCount(%classes) == 0)
      return %obj;
   %class = %obj.getClassName();
   for(%word = 0; %word < getWordCount(%classes); %word++)
   {
      if(%class $= getWord(%classes, %word))
         return %obj;
   }
   return -1;
}

// Sanitise client's control object.
function GameConnection::resolvePlayer(%client)
{
   if(!isObject(%client.player))
      return -1;
   if(%client.player.getClassName() !$= "Player")
      return -1;
   if(%client.getControlObject() != %client.player)
      return -1;
   return %client.player;
}

//-----------------------------------------------------------------------------
// Misc. server commands avialable to clients
//-----------------------------------------------------------------------------

function serverCmdInteract(%client)
{
   if(%client.getControlObject() == %client.player)
   {
   }
}
