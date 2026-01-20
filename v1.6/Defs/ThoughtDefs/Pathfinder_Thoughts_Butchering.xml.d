<?xml version="1.0" encoding="utf-8"?>
<Defs>
  <!-- A Pathfinder's thoughts when eating/butchering other sentients -->
  <ThoughtDef>
    <defName>Pathfinder_ButcheredHumanlikeCorpse</defName>
    <durationDays>6</durationDays>
    <stackLimit>10</stackLimit>
    <stackedEffectMultiplier>0.7</stackedEffectMultiplier>
    <nullifyingTraits>
      <li>Psychopath</li>
      <li>Bloodlust</li>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>I butchered a sentient</label>
        <description>I butchered a sentient being up like an animal.</description>
        <baseMoodEffect>-6</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>Pathfinder_KnowButcheredHumanlikeCorpse</defName>
    <durationDays>6</durationDays>
    <stackLimit>1</stackLimit>
    <nullifyingTraits>
      <li>Psychopath</li>
      <li>Bloodlust</li>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>we butchered humanlike</label>
        <description>We butchered up a sentient being like an animal.</description>
        <baseMoodEffect>-6</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>Pathfinder_AteHumanlikeMeatDirect</defName>
    <showBubble>true</showBubble>
    <icon>Things/Mote/ThoughtSymbol/Food</icon>
    <durationDays>1</durationDays>
    <nullifyingTraits>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>raw cannibalism</label>
        <description>I ate the meat of another sentient creature, raw, like an animal. This is a nightmare.</description>
        <baseMoodEffect>-18</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>Pathfinder_AteHumanlikeMeatAsIngredient</defName>
    <showBubble>true</showBubble>
    <icon>Things/Mote/ThoughtSymbol/Food</icon>
    <durationDays>1</durationDays>
    <nullifyingTraits>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>cooked cannibalism</label>
        <description>I ate a meal made from the meat of another sentient creature. This is horrible.</description>
        <baseMoodEffect>-14</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>
  
  <!-- Other races thoughts when butchering up a Pathfinder -->
  <ThoughtDef>
    <defName>ButcheredPathfinderCorpse</defName>
    <durationDays>6</durationDays>
    <stackLimit>10</stackLimit>
    <stackedEffectMultiplier>0.7</stackedEffectMultiplier>
    <nullifyingTraits>
      <li>Psychopath</li>
      <li>Bloodlust</li>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>I butchered a Pathfinder</label>
        <description>I butchered a Pathfinder, they may be feathered but it still felt wrong...</description>
        <baseMoodEffect>-5</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>KnowButcheredPathfinderCorpse</defName>
    <durationDays>6</durationDays>
    <stackLimit>1</stackLimit>
    <nullifyingTraits>
      <li>Psychopath</li>
      <li>Bloodlust</li>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>we butchered a Pathfinder</label>
        <description>They are an alien but that don't make it any better...</description>
        <baseMoodEffect>-5</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>AtePathfinderMeatDirect</defName>
    <showBubble>true</showBubble>
    <icon>Things/Mote/ThoughtSymbol/Food</icon>
    <durationDays>1</durationDays>
    <nullifyingTraits>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>raw cannibalism</label>
        <description>I ate the meat of another sentient creature, raw, like an animal. This is a nightmare.</description>
        <baseMoodEffect>-18</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>AtePathfinderAsIngredient</defName>
    <showBubble>true</showBubble>
    <icon>Things/Mote/ThoughtSymbol/Food</icon>
    <durationDays>1</durationDays>
    <nullifyingTraits>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>cooked cannibalism</label>
        <description>I ate a meal made from the meat of another sentient creature. This is horrible.</description>
        <baseMoodEffect>-14</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>
  
  <!-- A Pathfinder's thoughts when eating a Pathfinder/ butchering up a Pathfinder -->

  <ThoughtDef>
    <defName>Pathfinder_ButcheredPathfinderCorpse</defName>
    <durationDays>6</durationDays>
    <stackLimit>10</stackLimit>
    <stackedEffectMultiplier>0.7</stackedEffectMultiplier>
    <nullifyingTraits>
      <li>Psychopath</li>
      <li>Bloodlust</li>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>I butchered a Pathfinder</label>
        <description>I butchered one of our own. This is a can't be happening...</description>
        <baseMoodEffect>-8</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>Pathfinder_KnowButcheredPathfinderCorpse</defName>
    <durationDays>6</durationDays>
    <stackLimit>1</stackLimit>
    <nullifyingTraits>
      <li>Psychopath</li>
      <li>Bloodlust</li>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>we butchered a one of our own</label>
        <description>We butchered up a one of our own... it wasn't supposed to go like this...</description>
        <baseMoodEffect>-8</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>
  
  <ThoughtDef>
    <defName>Pathfinder_AtePathfinderMeatDirect</defName>
    <showBubble>true</showBubble>
    <icon>Things/Mote/ThoughtSymbol/Food</icon>
    <durationDays>1</durationDays>
    <nullifyingTraits>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>raw cannibalism</label>
        <description>I... I ate the meat of one of my own kind, raw, like an animal. This is a can't be happening...</description>
        <baseMoodEffect>-25</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>

  <ThoughtDef>
    <defName>Pathfinder_AtePathfinderAsIngredient</defName>
    <showBubble>true</showBubble>
    <icon>Things/Mote/ThoughtSymbol/Food</icon>
    <durationDays>1</durationDays>
    <nullifyingTraits>
      <li>Cannibal</li>
    </nullifyingTraits>
    <stages>
      <li>
        <label>cooked cannibalism</label>
        <description>I... I ate a meal made from the meat of form one of my own kind. This is horrifying...</description>
        <baseMoodEffect>-18</baseMoodEffect>
      </li>
    </stages>
  </ThoughtDef>
</Defs>