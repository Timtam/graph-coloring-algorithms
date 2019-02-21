#!/bin/sh

#test for all test instances
#python3 run_tests.py -t 1000 simulated-annealing > test_all.out 2> test_all.err < /dev/null;


#test for queuing jobs
#python3 run_tests.py -t 100 simulated-annealing > test1.out 2> test1.err < /dev/null;
#python3 run_tests.py -t  50 simulated-annealing > test2.out 2> test2.err < /dev/null;



#local-search
#python3 run_tests.py local-search > local_search.out 2> local_search.err < /dev/null;


#parameter test simulated-annealing start temperature
#python3 run_tests.py -t 300000 simulated-annealing -t  20 >  simulated-annealing_test20.out 2>  simulated-annealing_test20.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t  40 >  simulated-annealing_test40.out 2>  simulated-annealing_test40.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t  60 >  simulated-annealing_test60.out 2>  simulated-annealing_test60.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t  80 >  simulated-annealing_test80.out 2>  simulated-annealing_test80.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 100 > simulated-annealing_test100.out 2> simulated-annealing_test100.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 120 > simulated-annealing_test120.out 2> simulated-annealing_test120.err < /dev/null;


#simulated-annealing
#python3 run_tests.py -t  60000 simulated-annealing -t 20 -f 0.5 >  simulated-annealing/60_20_05.out 2>  simulated-annealing/60_20_05.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 20 -f 1.0 >  simulated-annealing/60_20_10.out 2>  simulated-annealing/60_20_10.err < /dev/null;
#python3 run_tests.py -t  60000 simulated-annealing -t 20 -f 2.0 >  simulated-annealing/60_20_20.out 2>  simulated-annealing/60_20_20.err < /dev/null;
#python3 run_tests.py -t  60000 simulated-annealing -t 30 -f 0.5 >  simulated-annealing/60_30_05.out 2>  simulated-annealing/60_30_05.err < /dev/null;
#python3 run_tests.py -t  60000 simulated-annealing -t 30 -f 1.0 >  simulated-annealing/60_30_10.out 2>  simulated-annealing/60_30_10.err < /dev/null;
#python3 run_tests.py -t  60000 simulated-annealing -t 30 -f 2.0 >  simulated-annealing/60_30_20.out 2>  simulated-annealing/60_30_20.err < /dev/null;
#python3 run_tests.py -t  60000 simulated-annealing -t 40 -f 0.5 >  simulated-annealing/60_40_05.out 2>  simulated-annealing/60_40_05.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 40 -f 1.0 >  simulated-annealing/60_40_10.out 2>  simulated-annealing/60_40_10.err < /dev/null;
#python3 run_tests.py -t  60000 simulated-annealing -t 40 -f 2.0 >  simulated-annealing/60_40_20.out 2>  simulated-annealing/60_40_20.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 20 -f 0.5 > simulated-annealing/300_20_05.out 2> simulated-annealing/300_20_05.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 20 -f 1.0 > simulated-annealing/300_20_10.out 2> simulated-annealing/300_20_10.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 20 -f 2.0 > simulated-annealing/300_20_20.out 2> simulated-annealing/300_20_20.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 30 -f 0.5 > simulated-annealing/300_30_05.out 2> simulated-annealing/300_30_05.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 30 -f 1.0 > simulated-annealing/300_30_10.out 2> simulated-annealing/300_30_10.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 30 -f 2.0 > simulated-annealing/300_30_20.out 2> simulated-annealing/300_30_20.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 40 -f 0.5 > simulated-annealing/300_40_05.out 2> simulated-annealing/300_40_05.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 40 -f 1.0 > simulated-annealing/300_40_10.out 2> simulated-annealing/300_40_10.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 40 -f 2.0 > simulated-annealing/300_40_20.out 2> simulated-annealing/300_40_20.err < /dev/null;
#python3 run_tests.py -t 600000 simulated-annealing -t 20 -f 0.5 > simulated-annealing/600_20_05.out 2> simulated-annealing/600_20_05.err < /dev/null;
#python3 run_tests.py -t 600000 simulated-annealing -t 20 -f 1.0 > simulated-annealing/600_20_10.out 2> simulated-annealing/600_20_10.err < /dev/null;
#python3 run_tests.py -t 600000 simulated-annealing -t 20 -f 2.0 > simulated-annealing/600_20_20.out 2> simulated-annealing/600_20_20.err < /dev/null;
#python3 run_tests.py -t 600000 simulated-annealing -t 30 -f 0.5 > simulated-annealing/600_30_05.out 2> simulated-annealing/600_30_05.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 30 -f 1.0 > simulated-annealing/600_30_10.out 2> simulated-annealing/600_30_10.err < /dev/null;
#python3 run_tests.py -t 600000 simulated-annealing -t 30 -f 2.0 > simulated-annealing/600_30_20.out 2> simulated-annealing/600_30_20.err < /dev/null;
#python3 run_tests.py -t 600000 simulated-annealing -t 40 -f 0.5 > simulated-annealing/600_40_05.out 2> simulated-annealing/600_40_05.err < /dev/null;
#python3 run_tests.py -t 600000 simulated-annealing -t 40 -f 1.0 > simulated-annealing/600_40_10.out 2> simulated-annealing/600_40_10.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 40 -f 2.0 > simulated-annealing/600_40_20.out 2> simulated-annealing/600_40_20.err < /dev/null;



#taboo-search
#python3 run_tests.py -t  60000 taboo-search        >  taboo-search/60_def.out 2>  taboo-search/60_def.err < /dev/null;
#python3 run_tests.py -t  60000 taboo-search -l  50 >  taboo-search/60_50.out  2>  taboo-search/60_50.err  < /dev/null;
#python3 run_tests.py -t  60000 taboo-search -l 200 >  taboo-search/60_200.out 2>  taboo-search/60_200.err < /dev/null;
#python3 run_tests.py -t 120000 taboo-search        > taboo-search/120_def.out 2> taboo-search/120_def.err < /dev/null;
#python3 run_tests.py -t 120000 taboo-search -l  50 > taboo-search/120_50.out  2> taboo-search/120_50.err  < /dev/null;
#python3 run_tests.py -t 120000 taboo-search -l 200 > taboo-search/120_200.out 2> taboo-search/120_200.err < /dev/null;
#python3 run_tests.py -t 300000 taboo-search        > taboo-search/300_def.out 2> taboo-search/300_def.err < /dev/null;
#python3 run_tests.py -t 300000 taboo-search -l  50 > taboo-search/300_50.out  2> taboo-search/300_50.err  < /dev/null;
#python3 run_tests.py -t 300000 taboo-search -l 200 > taboo-search/300_200.out 2> taboo-search/300_200.err < /dev/null;



#genetic-onepoint
#python3 run_tests.py -t 120000 genetic-onepoint -s  50 -p 0.025 > genetic-onepoint/120_50_025.out  2> genetic-onepoint/120_50_025.err  < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s  50 -p 0.050 > genetic-onepoint/120_50_050.out  2> genetic-onepoint/120_50_050.err  < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s  50 -p 0.100 > genetic-onepoint/120_50_100.out  2> genetic-onepoint/120_50_100.err  < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s 100 -p 0.025 > genetic-onepoint/120_100_025.out 2> genetic-onepoint/120_100_025.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s 100 -p 0.050 > genetic-onepoint/120_100_050.out 2> genetic-onepoint/120_100_050.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s 100 -p 0.100 > genetic-onepoint/120_100_100.out 2> genetic-onepoint/120_100_100.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s 200 -p 0.025 > genetic-onepoint/120_200_025.out 2> genetic-onepoint/120_200_025.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s 200 -p 0.050 > genetic-onepoint/120_200_050.out 2> genetic-onepoint/120_200_050.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-onepoint -s 200 -p 0.100 > genetic-onepoint/120_200_100.out 2> genetic-onepoint/120_200_100.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s  50 -p 0.025 > genetic-onepoint/600_50_025.out  2> genetic-onepoint/600_50_025.err  < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s  50 -p 0.050 > genetic-onepoint/600_50_050.out  2> genetic-onepoint/600_50_050.err  < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s  50 -p 0.100 > genetic-onepoint/600_50_100.out  2> genetic-onepoint/600_50_100.err  < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s 100 -p 0.025 > genetic-onepoint/600_100_025.out 2> genetic-onepoint/600_100_025.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s 100 -p 0.050 > genetic-onepoint/600_100_050.out 2> genetic-onepoint/600_100_050.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s 100 -p 0.100 > genetic-onepoint/600_100_100.out 2> genetic-onepoint/600_100_100.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s 200 -p 0.025 > genetic-onepoint/600_200_025.out 2> genetic-onepoint/600_200_025.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s 200 -p 0.050 > genetic-onepoint/600_200_050.out 2> genetic-onepoint/600_200_050.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-onepoint -s 200 -p 0.100 > genetic-onepoint/600_200_100.out 2> genetic-onepoint/600_200_100.err < /dev/null;



#genetic-twopoint
#python3 run_tests.py -t 120000 genetic-twopoint -s  50 -p 0.025 > genetic-twopoint/120_50_025.out  2> genetic-twopoint/120_50_025.err  < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s  50 -p 0.050 > genetic-twopoint/120_50_050.out  2> genetic-twopoint/120_50_050.err  < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s  50 -p 0.100 > genetic-twopoint/120_50_100.out  2> genetic-twopoint/120_50_100.err  < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s 100 -p 0.025 > genetic-twopoint/120_100_025.out 2> genetic-twopoint/120_100_025.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s 100 -p 0.050 > genetic-twopoint/120_100_050.out 2> genetic-twopoint/120_100_050.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s 100 -p 0.100 > genetic-twopoint/120_100_100.out 2> genetic-twopoint/120_100_100.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s 200 -p 0.025 > genetic-twopoint/120_200_025.out 2> genetic-twopoint/120_200_025.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s 200 -p 0.050 > genetic-twopoint/120_200_050.out 2> genetic-twopoint/120_200_050.err < /dev/null;
#python3 run_tests.py -t 120000 genetic-twopoint -s 200 -p 0.100 > genetic-twopoint/120_200_100.out 2> genetic-twopoint/120_200_100.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s  50 -p 0.025 > genetic-twopoint/600_50_025.out  2> genetic-twopoint/600_50_025.err  < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s  50 -p 0.050 > genetic-twopoint/600_50_050.out  2> genetic-twopoint/600_50_050.err  < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s  50 -p 0.100 > genetic-twopoint/600_50_100.out  2> genetic-twopoint/600_50_100.err  < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s 100 -p 0.025 > genetic-twopoint/600_100_025.out 2> genetic-twopoint/600_100_025.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s 100 -p 0.050 > genetic-twopoint/600_100_050.out 2> genetic-twopoint/600_100_050.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s 100 -p 0.100 > genetic-twopoint/600_100_100.out 2> genetic-twopoint/600_100_100.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s 200 -p 0.025 > genetic-twopoint/600_200_025.out 2> genetic-twopoint/600_200_025.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s 200 -p 0.050 > genetic-twopoint/600_200_050.out 2> genetic-twopoint/600_200_050.err < /dev/null;
#python3 run_tests.py -t 600000 genetic-twopoint -s 200 -p 0.100 > genetic-twopoint/600_200_100.out 2> genetic-twopoint/600_200_100.err < /dev/null;



#branch-bound
#python3 run_tests.py branch-bound > branch_bound.out 2> branch_bound.err < /dev/null;



exit 0
