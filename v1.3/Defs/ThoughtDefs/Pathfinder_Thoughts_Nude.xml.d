<?xml version="1.0" encoding="utf-8"?>
<Defs>
<ThoughtDef>
  <defName>PathfinderNaked</defName>
  <workerClass>ThoughtWorker_PsychologicallyNude</workerClass>
  <validWhileDespawned>true</validWhileDespawned>
  <nullifyingTraits>
    <li>Nudist</li>
  </nullifyingTraits>
  <stages>
    <li>
      <label>naked</label>
      <description>I'm naked. This wouldn't be so bad if it didn't feel like others were staring...</description>
      <baseMoodEffect>-1</baseMoodEffect>
    </li>
  </stages>
</ThoughtDef>

<ThoughtDef>
  <defName>PathfinderHappyNude</defName>
  <workerClass>ThoughtWorker_NudistNude</workerClass>
  <validWhileDespawned>true</validWhileDespawned>
  <requiredTraits>
    <li>Nudist</li>
  </requiredTraits>
  <stages>
    <li>
      <label>happily nude</label>
      <description>I'm totally exposed and loving it! I feel so free and full of confidence! I think some of the others are eyeing me up too...</description>
      <baseMoodEffect>15</baseMoodEffect>
    </li>
  </stages>
</ThoughtDef>
</Defs>