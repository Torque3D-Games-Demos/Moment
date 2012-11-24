function BasicBrain::onAdd(%this)
{
   AIPlayerBrain::onAdd(%this);
}

funcion BasicBrain::onEvent(%this, %obj, %event, %evtData)
{
   AIVocalPainResponse::onEvent(%this, %obj, %event, %evtData);
}
