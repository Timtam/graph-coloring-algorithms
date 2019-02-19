#!/bin/sh

#test for all test instances
#python3 run_tests.py -t 1000 simulated-annealing > test_all.out 2> test_all.err < /dev/null;


#test for queuing jobs
#python3 run_tests.py -t 100 simulated-annealing > test1.out 2> test1.err < /dev/null;
#python3 run_tests.py -t  50 simulated-annealing > test2.out 2> test2.err < /dev/null;



#local-search
python3 run_tests.py local-search >     local_search.out 2>     local_search.err < /dev/null;
#python3 run_tests.py local-search >  local_search_60.out 2>  local_search_60.err < /dev/null;
#python3 run_tests.py local-search > local_search_120.out 2> local_search_120.err < /dev/null;


#parameter test simulated-annealing start temperature
#python3 run_tests.py -t 300000 simulated-annealing -t  20 >  simulated-annealing_test20.out 2>  simulated-annealing_test20.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t  40 >  simulated-annealing_test40.out 2>  simulated-annealing_test40.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t  60 >  simulated-annealing_test60.out 2>  simulated-annealing_test60.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t  80 >  simulated-annealing_test80.out 2>  simulated-annealing_test80.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 100 > simulated-annealing_test100.out 2> simulated-annealing_test100.err < /dev/null;
#python3 run_tests.py -t 300000 simulated-annealing -t 120 > simulated-annealing_test120.out 2> simulated-annealing_test120.err < /dev/null;


#simulated-annealing
python3 run_tests.py -t  60000 simulated-annealing -t 20 -f 0.5 >  simulated-annealing/60_05.out 2>  simulated-annealing/60_05.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 20 -f 1.0 >  simulated-annealing/60_10.out 2>  simulated-annealing/60_10.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 20 -f 2.0 >  simulated-annealing/60_20.out 2>  simulated-annealing/60_20.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 30 -f 0.5 >  simulated-annealing/60_05.out 2>  simulated-annealing/60_05.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 30 -f 1.0 >  simulated-annealing/60_10.out 2>  simulated-annealing/60_10.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 30 -f 2.0 >  simulated-annealing/60_20.out 2>  simulated-annealing/60_20.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 40 -f 0.5 >  simulated-annealing/60_05.out 2>  simulated-annealing/60_05.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 40 -f 1.0 >  simulated-annealing/60_10.out 2>  simulated-annealing/60_10.err < /dev/null;
python3 run_tests.py -t  60000 simulated-annealing -t 40 -f 2.0 >  simulated-annealing/60_20.out 2>  simulated-annealing/60_20.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 20 -f 0.5 > simulated-annealing/300_05.out 2> simulated-annealing/300_05.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 20 -f 1.0 > simulated-annealing/300_10.out 2> simulated-annealing/300_10.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 20 -f 2.0 > simulated-annealing/300_20.out 2> simulated-annealing/300_20.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 30 -f 0.5 > simulated-annealing/300_05.out 2> simulated-annealing/300_05.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 30 -f 1.0 > simulated-annealing/300_10.out 2> simulated-annealing/300_10.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 30 -f 2.0 > simulated-annealing/300_20.out 2> simulated-annealing/300_20.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 40 -f 0.5 > simulated-annealing/300_05.out 2> simulated-annealing/300_05.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 40 -f 1.0 > simulated-annealing/300_10.out 2> simulated-annealing/300_10.err < /dev/null;
python3 run_tests.py -t 300000 simulated-annealing -t 40 -f 2.0 > simulated-annealing/300_20.out 2> simulated-annealing/300_20.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 20 -f 0.5 > simulated-annealing/600_05.out 2> simulated-annealing/600_05.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 20 -f 1.0 > simulated-annealing/600_10.out 2> simulated-annealing/600_10.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 20 -f 2.0 > simulated-annealing/600_20.out 2> simulated-annealing/600_20.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 30 -f 0.5 > simulated-annealing/600_05.out 2> simulated-annealing/600_05.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 30 -f 1.0 > simulated-annealing/600_10.out 2> simulated-annealing/600_10.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 30 -f 2.0 > simulated-annealing/600_20.out 2> simulated-annealing/600_20.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 40 -f 0.5 > simulated-annealing/600_05.out 2> simulated-annealing/600_05.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 40 -f 1.0 > simulated-annealing/600_10.out 2> simulated-annealing/600_10.err < /dev/null;
python3 run_tests.py -t 600000 simulated-annealing -t 40 -f 2.0 > simulated-annealing/600_20.out 2> simulated-annealing/600_20.err < /dev/null;

exit 0
