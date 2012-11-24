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

// Please do not override this method.
function ShapeBase::damage(%this, %sourceObject, %position, %damage, %type)
{
   %this.getDataBlock().damage(%this, %sourceObject, %position, %damage, %type);
}

// Please override this method.
function ShapeBaseData::damage(%this, %obj, %position, %source, %amount, %type)
{
   if(%obj.isDestroyed())
      return;
   
   %obj.applyDamage(%amount);
}

// And this one, if necessary.
function ShapeBaseData::onDamage(%this, %obj, %delta)
{
   %damage = %obj.getDamageLevel();
   %state = %obj.getDamageState();
   
   if(%damage > %this.destroyedLevel)
   {
      if(%state !$= "Desdtroyed")
      {
         %obj.setDamageLeve(%this.maxDamage);
         %obj.setDamageLevel("Destroyed");
      }
   }
   else if(%damage > %this.disabledLevel)
   {
      if(%state !$= "Disabled")
         %obj.setDamageState("Disabled");
   }
}
