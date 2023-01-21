﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Bang.Tests.Features.GameRules
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class DistributionDesCartesFeature : object, Xunit.IClassFixture<DistributionDesCartesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "CardDistribution.feature"
#line hidden
        
        public DistributionDesCartesFeature(DistributionDesCartesFeature.FixtureData fixtureData, Bang_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("fr-FR"), "Features/GameRules", "Distribution des cartes", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Distribution des cartes au lancement du jeu")]
        [Xunit.TraitAttribute("FeatureTitle", "Distribution des cartes")]
        [Xunit.TraitAttribute("Description", "Distribution des cartes au lancement du jeu")]
        public void DistributionDesCartesAuLancementDuJeu()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Distribution des cartes au lancement du jeu", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "playerName"});
                table1.AddRow(new string[] {
                            "Jean"});
                table1.AddRow(new string[] {
                            "Max"});
                table1.AddRow(new string[] {
                            "Emilie"});
                table1.AddRow(new string[] {
                            "Martin"});
#line 4
 testRunner.When("la partie est prête pour ces joueurs", ((string)(null)), table1, "Quand ");
#line hidden
#line 10
 testRunner.Then("\"Jean\" possède autant de cartes qu\'il a de points de vie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
#line 11
 testRunner.And("\"Max\" possède autant de cartes qu\'il a de points de vie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 12
 testRunner.And("\"Emilie\" possède autant de cartes qu\'il a de points de vie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 13
 testRunner.And("\"Martin\" possède autant de cartes qu\'il a de points de vie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                DistributionDesCartesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                DistributionDesCartesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
