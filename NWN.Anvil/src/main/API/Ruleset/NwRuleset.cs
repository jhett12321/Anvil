using System;
using System.Collections.Generic;
using Anvil.Services;
using NWN.Native.API;

namespace Anvil.API
{
  /// <summary>
  /// Ruleset definitions for the module.<br/>
  /// Classes/Feats/Races/Skills/BaseItems/Spells.
  /// </summary>
  public static class NwRuleset
  {
    /// <summary>
    /// Gets a list of all base item types defined in the module's ruleset.
    /// </summary>
    public static IReadOnlyList<NwBaseItem> BaseItems { get; private set; } = null!;

    /// <summary>
    /// Gets a list of all classes defined in the module's ruleset.
    /// </summary>
    public static IReadOnlyList<NwClass> Classes { get; private set; } = null!;

    /// <summary>
    /// Gets a list of all domains defined in the module's ruleset.
    /// </summary>
    public static IReadOnlyList<NwDomain> Domains { get; private set; } = null!;

    /// <summary>
    /// Gets a list of all feats defined in the module's ruleset.
    /// </summary>
    public static IReadOnlyList<NwFeat> Feats { get; private set; } = null!;

    /// <summary>
    /// Gets a list of all races defined in the module's ruleset.
    /// </summary>
    public static IReadOnlyList<NwRace> Races { get; private set; } = null!;

    /// <summary>
    /// Gets a list of all skills defined in the module's ruleset.
    /// </summary>
    public static IReadOnlyList<NwSkill> Skills { get; private set; } = null!;

    /// <summary>
    /// Gets a list of all spells defined in the module's ruleset.
    /// </summary>
    public static IReadOnlyList<NwSpell> Spells { get; private set; } = null!;

    /// <summary>
    /// Reloads all game rules (2da stuff, etc).<br/>
    /// @warning DANGER, DRAGONS. Bad things may or may not happen. Only use this if you know what you are doing.
    /// </summary>
    public static void ReloadRules()
    {
      NWNXLib.Rules().ReloadAll();
    }

    [ServiceBinding(typeof(Factory))]
    [ServiceBindingOptions(InternalBindingPriority.API)]
    internal sealed unsafe class Factory : IDisposable
    {
      private FunctionHook<ReloadAllHook>? reloadAllHook;

      public Factory(HookService hookService)
      {
        reloadAllHook = hookService.RequestHook<ReloadAllHook>(OnReloadAll, FunctionsLinux._ZN8CNWRules9ReloadAllEv, HookOrder.Latest);
        LoadRules();
      }

      private delegate void ReloadAllHook(void* pRules);

      void IDisposable.Dispose()
      {
        // ReloadAll is called from the CNWSModule destructor
        // If we don't dispose the hook here, the server will get stuck in an infinite segfault loop.
        reloadAllHook?.Dispose();
        reloadAllHook = null;
      }

      private static IReadOnlyList<NwBaseItem> LoadBaseItems(CNWBaseItemArray baseItemArray)
      {
        NwBaseItem[] retVal = new NwBaseItem[baseItemArray.m_nNumBaseItems];
        for (int i = 0; i < retVal.Length; i++)
        {
          retVal[i] = new NwBaseItem((uint)i, baseItemArray.GetBaseItem(i));
        }

        return retVal;
      }

      private static IReadOnlyList<NwClass> LoadClasses(CNWClassArray classArray, int count)
      {
        NwClass[] retVal = new NwClass[count];
        for (int i = 0; i < retVal.Length; i++)
        {
          retVal[i] = new NwClass((byte)i, classArray.GetItem(i));
        }

        return retVal;
      }

      private static IReadOnlyList<NwFeat> LoadFeats(CNWFeatArray featArray, int count)
      {
        NwFeat[] retVal = new NwFeat[count];
        for (int i = 0; i < retVal.Length; i++)
        {
          retVal[i] = new NwFeat((ushort)i, featArray.GetItem(i));
        }

        return retVal;
      }

      private static IReadOnlyList<NwRace> LoadRaces(CNWRaceArray raceArray, int count)
      {
        NwRace[] retVal = new NwRace[count];
        for (int i = 0; i < retVal.Length; i++)
        {
          retVal[i] = new NwRace((ushort)i, raceArray.GetItem(i));
        }

        return retVal;
      }

      private static void LoadRules()
      {
        CNWRules rules = NWNXLib.Rules();
        Races = LoadRaces(CNWRaceArray.FromPointer(rules.m_lstRaces), rules.m_nNumRaces);
        Classes = LoadClasses(CNWClassArray.FromPointer(rules.m_lstClasses), rules.m_nNumClasses);
        Skills = LoadSkills(CNWSkillArray.FromPointer(rules.m_lstSkills), rules.m_nNumSkills);
        Feats = LoadFeats(CNWFeatArray.FromPointer(rules.m_lstFeats), rules.m_nNumFeats);
        BaseItems = LoadBaseItems(rules.m_pBaseItemArray);
        Spells = LoadSpells(rules.m_pSpellArray);
        Domains = LoadDomains(CNWDomainArray.FromPointer(rules.m_lstDomains), rules.m_nNumDomains);
      }

      private static IReadOnlyList<NwSkill> LoadSkills(CNWSkillArray skillArray, int count)
      {
        NwSkill[] retVal = new NwSkill[count];
        for (int i = 0; i < retVal.Length; i++)
        {
          retVal[i] = new NwSkill((byte)i, skillArray.GetItem(i));
        }

        return retVal;
      }

      private static IReadOnlyList<NwSpell> LoadSpells(CNWSpellArray spellArray)
      {
        NwSpell[] retVal = new NwSpell[spellArray.m_nNumSpells];
        for (int i = 0; i < retVal.Length; i++)
        {
          retVal[i] = new NwSpell(i, spellArray.GetSpell(i));
        }

        return retVal;
      }

      private static IReadOnlyList<NwDomain> LoadDomains(CNWDomainArray domainArray, int count)
      {
        NwDomain[] retVal = new NwDomain[count];
        for (int i = 0; i < retVal.Length; i++)
        {
          retVal[i] = new NwDomain((byte)i, domainArray.GetItem(i));
        }

        return retVal;
      }

      private void OnReloadAll(void* pRules)
      {
        if (reloadAllHook != null)
        {
          reloadAllHook.CallOriginal(pRules);
          LoadRules();
        }
      }
    }
  }
}
