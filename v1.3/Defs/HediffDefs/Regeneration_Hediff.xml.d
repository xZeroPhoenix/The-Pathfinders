<?xml version="1.0" encoding="utf-8" ?>
<Defs>
  <HeddiffDef>
    <defName>PathfinderRegeneration</defName>
    <hediffClass>HediffWithComps</hediffClass>
  <defaultLabelColor>(0.3, 0.7, 0.7)</defaultLabelColor>
  <description>Pathfinders are able to regenerated limbs and orgens as long as they have not suffered brain death or total cardiovascular failure. This process can be quite lengthy and arduous depending on the severity of the wound.</description>
  <label>Pathfinder Regeneration</label>
  <isBad>false</isBad>
  <scenarioCanAdd>false</scenarioCanAdd>
  <minSeverity>1</minSeverity>
  <maxSeverity>1</maxSeverity>
  <initialSeverity>1</initialSeverity>
    <comps>
      
    </comps>
  </HeddiffDef>
  
  <HediffDef>
    <defName>PATH_regenerating</defName>
    <hediffClass>Pathfinder.Regenerating_Hediff</hediffClass>
    <defaultLabelColor>(0.15, 0.6, 0.2)</defaultLabelColor>
    <description>This body part is regenerating, it will be largely non-functional until nearly fully regrown and will be a significant drain on the bodies resources.</description>
    <isBad>false</isBad>
    <label>Regrowing</label>
    <stages>
      <li>
        <label>stitching</label>
        <becomeVisible>true</becomeVisible>
        <partEfficiencyOffset>-1.0</partEfficiencyOffset>
      </li>
      <li>
        <label>growing</label>
        <becomeVisible>true</becomeVisible>
        <partEfficiencyOffset>-1.0</partEfficiencyOffset>
        <minSeverity>0.1</minSeverity>
      </li>
      <li>
        <label>shaping</label>
        <becomeVisible>true</becomeVisible>
        <partEfficiencyOffset>-0.75</partEfficiencyOffset>
        <minSeverity>0.5</minSeverity>
      </li>
      <li>
        <label>developing</label>
        <becomeVisible>true</becomeVisible>
        <partEfficiencyOffset>-0.5</partEfficiencyOffset>
        <minSeverity>0.75</minSeverity>
      </li>
    </stages>
  </HediffDef>

  <HediffDef ParentName="DiseaseBase">
    <defName>PATH_adjusting</defName>
    <defaultLabelColor>(0.75, 1, 0.75)</defaultLabelColor>
    <description>This body part has just finished regenerating, it is still adjusting to its normal use and will be at normal capacity within a day.</description>
    <label>Adjusting</label>
    <stages>
      <li>
        <partEfficiencyOffset>-0.1</partEfficiencyOffset>
      </li>
    </stages>
    <comps>
      <li Class="HediffCompProperties_Disappears">
        <disappearsAfterTicks>
          <min>20000</min>
          <max>60000</max>
        </disappearsAfterTicks>
      </li>
    </comps>
  </HediffDef>
  
  
  
  
</Defs>
